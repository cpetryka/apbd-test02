using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Test02.DTOs;
using Test02.Models;
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
    
    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItems(int characterId, List<int> itemsId)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
        {
            return NotFound($"Character with given id = {characterId} doesn't exist");
        }

        if (!await _dbService.DoAllItemsExist(itemsId))
        {
            return NotFound($"Not all items exist");
        }
        
        if(_dbService.SumItemsWeight(itemsId) > _dbService.GetCharacterFreeSpace(characterId))
        {
            return NotFound($"Items weight too much");
        }

        List<Backpack> backpacks = new List<Backpack>();

        foreach (var itemId in itemsId)
        {
            backpacks.Add(new Backpack()
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            });
        }
        
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddBackpacks(backpacks);
            scope.Complete();
        }
        
        return Created();
    }
}