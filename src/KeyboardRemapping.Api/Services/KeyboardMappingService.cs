using KeyboardRemapping.Api.Contracts;
using KeyboardRemapping.Api.Data;
using KeyboardRemapping.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyboardRemapping.Api.Services;

/// <summary>
/// Service permettant de gérer les mappings de touches d'un clavier
/// </summary>
public class KeyboardMappingService : IKeyboardMappingService
{
    /// <summary>
    /// Contexte de la base de données
    /// </summary>
    private readonly KeyboardDbContext _db;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="db">Contexte de la base de données</param>
    public KeyboardMappingService(KeyboardDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Retourne la liste des claviers connus
    /// </summary>    
    public async Task<IReadOnlyList<KeyboardResponse>> GetKeyboardsAsync()
    {
        return await _db.Keyboards.OrderBy(x => x.Name)
                                    .Select(x => new KeyboardResponse
                                    {
                                        Id = x.Id,
                                        Name = x.Name
                                    })
                                    .ToListAsync();
    }

    /// <summary>
    /// Crée un nouveau clavier avec les touches disponibles.
    /// </summary>
    /// <param name="keyboardName">Nom du clavier à créer.</param>
    public async Task CreateKeyboardAsync(string keyboardName)
    {
        var keyboardExists = await _db.Keyboards.AnyAsync(x => x.Name == keyboardName);

        if (keyboardExists)
            throw new ArgumentException($"Keyboard '{keyboardName}' already exists.");


        // Creation du clavier et on lui affecte ses touches
        var keyboard = new Keyboard
        {
            Name = keyboardName,
            Keys = HidKeyCatalog.All.Select(key => new KeyboardKey
                                    {
                                        Name = key.Name,
                                        HidUsageCode = key.HidUsageCode
                                    })
                                    .ToList()
        };

        _db.Keyboards.Add(keyboard);
        await _db.SaveChangesAsync();
    }

   

    /// <summary>
    /// Supprime un clavier via son identifiant
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    public async Task DeleteKeyboardAsync(int keyboardId)
    {
        var keyboard = await _db.Keyboards.FirstOrDefaultAsync(x => x.Id == keyboardId);

        if (keyboard == null)
            throw new KeyNotFoundException($"KeyboardId '{keyboardId}' not found.");

        _db.Keyboards.Remove(keyboard);
        await _db.SaveChangesAsync();
    }


     /// <summary>
    /// Retourne les mappings d'un clavier via son identifiant
    /// </summary>
    /// <param name="keyboardName">Nom du clavier</param>
    /// <returns>Liste des mappings du clavier</returns>
    public async Task<IReadOnlyList<KeyMappingResponse>> GetMappingsByKeyboardIdAsync(int keyboardId)
    {
        // Recherche du clavier et de ses touches
        var keyboard = await _db.Keyboards.Include(x => x.Keys)
                                            .FirstOrDefaultAsync(x => x.Id == keyboardId);

        if (keyboard == null)
            throw new KeyNotFoundException($"KeyboardId '{keyboardId}' not found.");

        // Récupération des mappings existants
        var mappings = await _db.KeyMappings.Where(x => x.KeyboardId == keyboard.Id)
                                            .Include(x => x.SourceKey)
                                            .Include(x => x.TargetKey)
                                            .ToListAsync();

        // Permet de retrouver rapidement le mapping à partir de la touche source
        var mappingsBySource = mappings.ToDictionary(x => x.SourceKeyId);

        // Retourne uniquement les touches qui ont un mapping
        return keyboard.Keys.Where(key => mappingsBySource.ContainsKey(key.Id))
                            .OrderBy(x => x.HidUsageCode)
                            .Select(key =>
                            {
                                mappingsBySource.TryGetValue(key.Id, out var mapping);

                                return new KeyMappingResponse
                                {
                                    SourceHidUsageCode = key.HidUsageCode,
                                    SourceKeyName = key.Name,
                                    TargetHidUsageCode = mapping?.TargetKey.HidUsageCode,
                                    TargetKeyName = mapping?.TargetKey.Name
                                };
                            })
                            .ToList();
    }

    /// <summary>
    /// Ajoute ou met à jour les mappings d'un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <param name="request">Liste des mappings à ajouter ou mettre à jour</param>
    public async Task SetMappingsByKeyboardIdAsync(int keyboardId, SetMappingsRequest request)
    {
        var keyboard = await _db.Keyboards.Include(x => x.Keys)
                                            .FirstOrDefaultAsync(x => x.Id == keyboardId);

        if (keyboard == null)
            throw new KeyNotFoundException($"KeyboardId '{keyboardId}' not found.");

        // Création d'un dictionnaire pour retrouver rapidement une touche avec son code HID en clé
        var keysByHid = keyboard.Keys.ToDictionary(x => x.HidUsageCode);

        // -----------------------------------------------------------------------------------------
        // Régles metier ou j'ai voulu que ça soit du 1 pour 1.
        // Une touche pour une autre touche. et pas N touche source pour 1 cible
        // -----------------------------------------------------------------------------------------

        // Vérification si une même touche source utilisée par plusieurs mappings
        var duplicateSources = request.Mappings.GroupBy(x => x.SourceHidUsageCode)
                                                .Where(x => x.Count() > 1)
                                                .Select(x => x.Key)
                                                .ToList();

        if (duplicateSources.Count > 0)
        {
            throw new ArgumentException("A source key can only appear once in the mappings.");
        }

        // Vérification si une même touche cible utilisée par plusieurs mappings
        var duplicateTargets = request.Mappings.GroupBy(x => x.TargetHidUsageCode)
                                                .Where(x => x.Count() > 1)
                                                .Select(x => x.Key)
                                                .ToList();

        if (duplicateTargets.Count > 0)
        {
            throw new ArgumentException("A target key can only be used once in the mappings.");
        }

    
        // Vérification qu'une touche cible n'est pas déjà utilisée par un autre mapping.
        foreach (var mapping in request.Mappings)
        {
            var sourceKeyId = keysByHid[mapping.SourceHidUsageCode].Id;
            var targetKeyId = keysByHid[mapping.TargetHidUsageCode].Id;

            var targetAlreadyUsed = await _db.KeyMappings.AnyAsync(x => x.KeyboardId == keyboard.Id 
                                                                    && x.TargetKeyId == targetKeyId 
                                                                    && x.SourceKeyId != sourceKeyId);

            if (targetAlreadyUsed)
            {
                throw new ArgumentException($"Target HID usage code '{mapping.TargetHidUsageCode}' is already used by another mapping.");
            }
        }

        // -----------------------------------------------------------------------------------------

        // Vérification que les codes HID utilisés existent bien sur le clavier.
        foreach (var mapping in request.Mappings)
        {
            if (!keysByHid.ContainsKey(mapping.SourceHidUsageCode))
            {
                throw new ArgumentException($"Unknown source HID usage code: {mapping.SourceHidUsageCode}.");
            }

            if (!keysByHid.ContainsKey(mapping.TargetHidUsageCode))
            {
                throw new ArgumentException($"Unknown target HID usage code: {mapping.TargetHidUsageCode}.");
            }
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();

        foreach (var mapping in request.Mappings)
        {
            var sourceKeyId = keysByHid[mapping.SourceHidUsageCode].Id;
            var targetKeyId = keysByHid[mapping.TargetHidUsageCode].Id;

            var existingMapping = await _db.KeyMappings.FirstOrDefaultAsync(x => x.KeyboardId == keyboard.Id 
                                                                            && x.SourceKeyId == sourceKeyId);
            
            // Si le mapping n'est pas connu, on l'ajoute
            if (existingMapping == null)
            {
                _db.KeyMappings.Add(new KeyMapping
                {
                    KeyboardId = keyboard.Id,
                    SourceKeyId = sourceKeyId,
                    TargetKeyId = targetKeyId
                });
            }
            // Si le mapping est trouvé on le mets a jour
            else
            {
                existingMapping.TargetKeyId = targetKeyId;
            }
        }

        await _db.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    /// <summary>
    /// Supprime tous les mappings d'un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    public async Task DeleteMappingsByKeyboardIdAsync(int keyboardId)
    {
        var keyboardExists = await _db.Keyboards.AnyAsync(x => x.Id == keyboardId);

        if (!keyboardExists)
            throw new KeyNotFoundException($"KeyboardId '{keyboardId}' not found.");

        await _db.KeyMappings.Where(x => x.KeyboardId == keyboardId)
                            .ExecuteDeleteAsync();
    }
}