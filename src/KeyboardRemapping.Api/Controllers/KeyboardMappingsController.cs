using KeyboardRemapping.Api.Contracts;
using KeyboardRemapping.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeyboardRemapping.Api.Controllers;

/// <summary>
/// Point d'entrée de l'API 
/// </summary>
[ApiController]
[Route("api/keyboards")]
public class KeyboardMappingsController : ControllerBase
{
    private readonly IKeyboardMappingService _mappingService;

    /// <summary>
    /// Constructueur
    /// </summary>
    /// <param name="mappingService"></param>
    public KeyboardMappingsController(IKeyboardMappingService mappingService)
    {
        _mappingService = mappingService;
    }

    /// <summary>
    /// Methode exposée pour récupérer la liste des claviers connus
    /// </summary>
    /// <returns>Liste des claviers avec leur identifiant et leur nom</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<KeyboardResponse>>> GetKeyboards()
    {
        var keyboards = await _mappingService.GetKeyboardsAsync();
        return Ok(keyboards);
    }


    /// <summary>
    /// Methode exposée pour créér un clavier en base de données
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateKeyboard([FromBody] CreateKeyboardRequest request)
    {
        try
        {
            await _mappingService.CreateKeyboardAsync(request.Name);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Methode exposée pour supprimer un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <returns>Réponse indiquant si le clavier a été supprimé</returns>
    [HttpDelete("{keyboardId}")]
    public async Task<IActionResult> DeleteKeyboard(int keyboardId)
    {
        try
        {
            await _mappingService.DeleteKeyboardAsync(keyboardId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Methode exposée pour récupérer le mapping existant via l'identifiant d'un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <returns></returns>
    [HttpGet("{keyboardId}/mappings")]
    public async Task<ActionResult<IReadOnlyList<KeyMappingResponse>>> GetMappingsByKeyboardId(int keyboardId)
    {
        try
        {
            var mappings = await _mappingService.GetMappingsByKeyboardIdAsync(keyboardId);
            return Ok(mappings);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
    }

    /// <summary>
    /// Methode exposée pour appliquer un mapping sur un clavier via son identifiant
    /// </summary>
    /// <param name="keyboardId">Nom du clavier</param>
    /// <param name="request">Mapping qu'on souhaite lui attribuer</param>
    /// <returns></returns>
    [HttpPut("{keyboardId}/mappings")]
    public async Task<IActionResult> SetMappingsByKeyboardId(int keyboardId, [FromBody] SetMappingsRequest request)
    {
        try
        {
            await _mappingService.SetMappingsByKeyboardIdAsync(keyboardId,request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {message = ex.Message });
        }
    }

    /// <summary>
    /// Methode exposée pour supprimer les mappings d'un clavier
    /// </summary>
    /// <param name="keyboardId">Identifiant du clavier</param>
    /// <returns>Réponse indiquant si les mappings ont été supprimés</returns>
    [HttpDelete("{keyboardId}/mappings")]
    public async Task<IActionResult> DeleteMappings(int keyboardId)
    {
        try
        {
            await _mappingService.DeleteMappingsByKeyboardIdAsync(keyboardId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}