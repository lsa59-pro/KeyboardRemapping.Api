namespace KeyboardRemapping.Api.Contracts;

/// <summary>
/// Classe qui représente une demande de mapping entre deux touches
/// </summary>
public class KeyMappingRequest
{
    /// <summary>
    ///  Code HID de la touche d'origine
    /// </summary>
    public int SourceHidUsageCode { get; set; }

    /// <summary>
    /// Code HID de la touche cible
    /// </summary>
    public int TargetHidUsageCode { get; set; }
}