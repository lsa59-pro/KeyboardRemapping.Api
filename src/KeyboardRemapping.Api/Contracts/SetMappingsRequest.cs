namespace KeyboardRemapping.Api.Contracts;

/// <summary>
/// Classe qui représente une demande de sauvegarde des mappings
/// </summary>
public class SetMappingsRequest
{
    /// <summary>
    /// Liste des mappings à sauvegarder
    /// </summary>
    public List<KeyMappingRequest> Mappings { get; set; } = [];
}