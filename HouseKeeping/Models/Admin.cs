using System.ComponentModel.DataAnnotations;
namespace HouseKeeping.Models;

public class Admin
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}