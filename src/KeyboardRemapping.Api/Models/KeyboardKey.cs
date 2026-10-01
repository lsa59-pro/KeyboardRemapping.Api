namespace KeyboardRemapping.Api.Models;

/// <summary>
/// Classe qui représente une touche du clavier.
/// </summary>
public class KeyboardKey
{
    /// <summary>
    /// Identifiant de la touche.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifiant du clavier auquel appartient la touche
    /// </summary>
    public int KeyboardId { get; set; }

    /// <summary>
    /// Nom de la touche (ce qui est affiché sur le clavier)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Code HID permettant d'identifier la touche.
    /// </summary>
    public int HidUsageCode { get; set; }

    /// <summary>
    /// Référence à l'objet Keyboard auquel appartient la touche
    /// </summary>
    public Keyboard Keyboard { get; set; } = null!;

    /// <summary>
    /// Liste des mappings utilisant cette touche comme touche d'origine
    /// </summary>
    public ICollection<KeyMapping> SourceMappings { get; set; } = [];

    /// <summary>
    /// Liste des mappings utilisant cette touche comme touche cible
    /// </summary>
    public ICollection<KeyMapping> TargetMappings { get; set; } = [];
}