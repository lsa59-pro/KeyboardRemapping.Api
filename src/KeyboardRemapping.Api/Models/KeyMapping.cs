namespace KeyboardRemapping.Api.Models;

/// <summary>
/// Classe qui représente un Mapping de touche
/// </summary>
public class KeyMapping
{
    /// <summary>
    /// Identifiant de mapping
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant du clavier
    /// </summary>
    public int KeyboardId { get; set; }

    /// <summary>
    /// Identifiant de la touche d'origine du clavier
    /// </summary>
    public int SourceKeyId { get; set; }

    /// <summary>
    /// Identifiant de la touche cible du clavier
    /// </summary>
    public int TargetKeyId { get; set; }

    /// <summary>
    /// Référence vers le clavier concerné par le mappage.
    /// </summary>
    public Keyboard Keyboard { get; set; } = null!;

    /// <summary>
    /// Référence à l'objet KeyboardKey correspondant à la touche d'origine
    /// </summary>
    public KeyboardKey SourceKey { get; set; } = null!;

    /// <summary>
    /// Référence à l'objet KeyboardKey correspondant à la touche cible
    /// </summary>
    public KeyboardKey TargetKey { get; set; } = null!;
}