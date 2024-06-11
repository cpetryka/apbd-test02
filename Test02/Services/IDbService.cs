using Test02.DTOs;
using Test02.Models;

namespace Test02.Services;

public interface IDbService
{
    Task<ICollection<Character>> GetCharacterData(int idx);
    Task<bool> DoesCharacterExist(int characterId);
    Task<bool> DoesItemExist(int itemId);
    Task<bool> DoAllItemsExist(List<int> itemsId);
    int GetCharacterFreeSpace(int characterId);
    int SumItemsWeight(List<int> itemsId);
    Task AddBackpacks(IEnumerable<Backpack> backpacks);
}