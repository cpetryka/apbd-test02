using Microsoft.EntityFrameworkCore;
using Test02.Data;
using Test02.DTOs;
using Test02.Models;

namespace Test02.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Character>> GetCharacterData(int idx)
    {
        return await _context.Characters
            .Include(c => c.Backpacks)
                .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
                .ThenInclude(ct => ct.Title)
            .Where(c => c.Id == idx)
            .ToListAsync();
    }

    public async Task<bool> DoesCharacterExist(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }

    public async Task<bool> DoesItemExist(int itemId)
    {
        return await _context.Items.AnyAsync(i => i.Id == itemId);
    }

    public async Task<bool> DoAllItemsExist(List<int> itemsId)
    {
        foreach (var itemId in itemsId)
        {
            if (!await DoesItemExist(itemId))
            {
                return false;
            }
        }

        return true;
    }

    public int GetCharacterFreeSpace(int characterId)
    {
        var character = _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId).Result;

        return character.MaxWeight - character.CurrentWeight;
    }

    public int SumItemsWeight(List<int> itemsId)
    {
        return _context.Items
            .Where(item => itemsId.Contains(item.Id))
            .Sum(item => item.Weight);
    }

    public async Task AddBackpacks(IEnumerable<Backpack> backpacks)
    {
        await _context.AddRangeAsync(backpacks);
        await _context.SaveChangesAsync();
    }
}