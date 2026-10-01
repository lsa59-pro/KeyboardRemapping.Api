namespace KeyboardRemapping.Api.Contracts;

/// <summary>
/// Classe qui représente les informations d'un clavier
/// </summary>
public class KeyboardResponse
{
    /// <summary>
    /// Identifiant du clavier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du clavier
    /// </summary>
    public string Name { get; set; } = string.Empty;
}