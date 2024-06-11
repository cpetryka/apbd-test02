using Microsoft.EntityFrameworkCore;
using Test02.Models;

namespace Test02.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().HasData(new List<Item>
        {
            new Item
            {
                Id = 1,
                Name = "Item 1",
                Weight = 10
            },
            new Item
            {
                Id = 2,
                Name = "Item 2",
                Weight = 20
            },
            new Item
            {
                Id = 3,
                Name = "Item 3",
                Weight = 30
            },
            new Item
            {
                Id = 4,
                Name = "Item 4",
                Weight = 40
            }
        });
        
        modelBuilder.Entity<Character>().HasData(new List<Character>
        {
            new Character
            {
                Id = 1,
                FirstName = "Character first name 1",
                LastName = "Character last Name 1",
                CurrentWeight = 10,
                MaxWeight = 20
            },
            new Character
            {
                Id = 2,
                FirstName = "Character first name 2",
                LastName = "Character last ame 2",
                CurrentWeight = 20,
                MaxWeight = 25
            }
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
        {
            new Backpack
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 10
            },
            new Backpack
            {
                CharacterId = 2,
                ItemId = 2,
                Amount = 20
            },
            new Backpack
            {
                CharacterId = 1,
                ItemId = 3,
                Amount = 30
            }
        });
        
        modelBuilder.Entity<Title>().HasData(new List<Title>
        {
            new Title
            {
                Id = 1,
                Name = "Title name 1"
            },
            new Title
            {
                Id = 2,
                Name = "Title name 2"
            },
            new Title
            {
                Id = 3,
                Name = "Title name 3"
            }
        });
        
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
        {
            new CharacterTitle
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Parse("2024-05-31")
            },
            new CharacterTitle
            {
                CharacterId = 2,
                TitleId = 2,
                AcquiredAt = DateTime.Parse("2024-06-10")
            }
        });
    }
}
