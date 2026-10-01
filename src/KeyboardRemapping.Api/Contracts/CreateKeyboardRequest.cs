namespace KeyboardRemapping.Api.Contracts;

/// <summary>
/// Classe qui représente une demande de création d'un clavier
/// </summary>
public class CreateKeyboardRequest
{
    /// <summary>
    /// Nom du clavier à créer
    /// </summary>
    public string Name { get; set; } = string.Empty;
}