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
}