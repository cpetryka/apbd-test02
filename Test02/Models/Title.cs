using System.ComponentModel.DataAnnotations;

namespace Test02.Models;

public class Title
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
}