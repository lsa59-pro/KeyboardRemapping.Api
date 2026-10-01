namespace KeyboardRemapping.Api.Models;

/// <summary>
/// Classe qui modélise un clavier
/// </summary>
public class Keyboard
{
    /// <summary>
    /// Identifiant du clavier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Modele de clavier
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Liste des touches du clavier
    /// </summary>
    public ICollection<KeyboardKey> Keys { get; set; } = [];
}