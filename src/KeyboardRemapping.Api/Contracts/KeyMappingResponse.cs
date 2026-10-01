namespace KeyboardRemapping.Api.Contracts;

/// <summary>
/// Classe qui représente la réponse de l'api d'un mapping de touche.
/// </summary>
public class KeyMappingResponse
{
    /// <summary>
    ///  Code HID de la touche d'origine
    /// </summary>
    public int SourceHidUsageCode { get; set; }

    /// <summary>
    ///  Nom de la touche d'origine (ce qui est noté sur le clavier)
    /// </summary>
    public string SourceKeyName { get; set; } = string.Empty;

    /// <summary>
    /// Code HID de la touche cible, si un mapping existe
    /// </summary>
    public int? TargetHidUsageCode { get; set; }

    /// <summary>
    /// Nom de la touche cible, si un mapping existe.
    /// </summary>
    public string? TargetKeyName { get; set; }
}