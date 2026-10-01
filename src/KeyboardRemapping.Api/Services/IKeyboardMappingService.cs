using KeyboardRemapping.Api.Contracts;

namespace KeyboardRemapping.Api.Services;

/// <summary>
/// Interface du service de gestion des mappings de touches
/// </summary>
public interface IKeyboardMappingService
{
    /// <summary>
    /// Crée un nouveau clavier avec ses touches disponibles
    /// </summary>
    /// <param name="keyboardName">Nom du clavier à créer</param>
    Task CreateKeyboardAsync(string keyboardName);

    /// <summary>
    /// Retourne la liste des claviers connus
    /// </summary>
    /// <returns>Liste des claviers avec leur identifiant et leur nom</returns>
    Task<IReadOnlyList<KeyboardResponse>> GetKeyboardsAsync();

    /// <summary>
    /// Supprime un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    Task DeleteKeyboardAsync(int keyboardId);

    /// <summary>
    /// Enregistre les mappings pour un clavier via son identifiant
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <param name="request">Liste des mappings à enregistrer</param>
    Task SetMappingsByKeyboardIdAsync(int keyboardId, SetMappingsRequest request);

    /// <summary>
    /// /// <summary>
    /// Retourne les mappings d'un clavier via son identifiant
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <returns>Liste des mappings du clavier</returns>
    Task<IReadOnlyList<KeyMappingResponse>> GetMappingsByKeyboardIdAsync(int keyboardId);

    /// <summary>
    /// Supprime tous les mappings d'un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    Task DeleteMappingsByKeyboardIdAsync(int keyboardId);
}