using Test02.DTOs;
using Test02.Models;

namespace Test02.Services;

public interface IDbService
{
    Task<ICollection<Character>> GetCharacterData(int idx);
}