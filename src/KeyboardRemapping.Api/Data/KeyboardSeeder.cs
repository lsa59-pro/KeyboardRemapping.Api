using KeyboardRemapping.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyboardRemapping.Api.Data;

/// <summary>
/// Classe permettant d'initialiser la base de données avec les données du clavier "Apex Pro Gen 3"
/// </summary>
public static class KeyboardSeeder
{
    /// <summary>
    /// Nom du clavier à ajouter dans la base de données.
    /// </summary>
    private const string KeyboardName = "Apex Pro Gen 3";

    /// <summary>
    /// Initialise la base de données avec le clavier et ses touches.
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static async Task SeedAsync(KeyboardDbContext db)
    {
        // Si un clavier existe déjà, on ne fait rien
        if (await db.Keyboards.AnyAsync())
            return;

        var keyboard = new Keyboard
        {
            Name = KeyboardName,
            Keys = HidKeyCatalog.All
                .Select(key => new KeyboardKey
                {
                    Name = key.Name,
                    HidUsageCode = key.HidUsageCode
                })
                .ToList()
        };

        // Ajout et sauvegarde dans la bdd
        db.Keyboards.Add(keyboard);
        await db.SaveChangesAsync();
    }
}