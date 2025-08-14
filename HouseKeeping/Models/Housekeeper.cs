using System.ComponentModel.DataAnnotations;
namespace HouseKeeping.Models;
public class Housekeeper
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public bool Active { get; set; } = true;

    public ICollection<DailyJobEntry> JobEntries { get; set; }
}
