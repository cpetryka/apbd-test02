namespace Test02.DTOs;

public class GetCharacterDto
{
    public string FirstName { get; set; }
    public string FLastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<GetBackpackItemDto> BackpackItems { get; set; } = null!;
    public ICollection<GetTitleDto> Titles { get; set; } = null!;
}

public class GetBackpackItemDto {
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class GetTitleDto
{
    public string Title { get; set; }
    public DateTime AcquiredAt { get; set; }
}