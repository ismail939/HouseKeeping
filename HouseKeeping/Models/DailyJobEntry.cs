using System.ComponentModel.DataAnnotations;
namespace HouseKeeping.Models;
public class DailyJobEntry
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly Date { get; set; } 
    [Required]
    public string DayWork { get; set; }

    // Foreign key to housekeeper
    public int HousekeeperId { get; set; }
    public Housekeeper Housekeeper { get; set; }
}
