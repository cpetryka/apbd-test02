using Microsoft.AspNetCore.Mvc;
using Test02.DTOs;
using Test02.Services;

namespace Test02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetOrdersData(int characterId)
    {
        var characters = await _dbService.GetCharacterData(characterId);

        return Ok(characters.Select(c => new GetCharacterDto()
        {
            FirstName = c.FirstName,
            FLastName = c.LastName,
            CurrentWeight = c.CurrentWeight,
            MaxWeight = c.MaxWeight,
            BackpackItems = c.Backpacks.Select(b => new GetBackpackItemDto()
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = c.CharacterTitles.Select(ct => new GetTitleDto()
            {
                Title = ct.Title.Name,
                AcquiredAt = ct.AcquiredAt
            }).ToList()
        }));
    }
}