using System.ComponentModel.DataAnnotations;

namespace Source.Models;

public class DriveFolder
{
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string UserID { get; set; } = "";
  public string Name { get; set; } = "";
  public List<DriveFile>? Files { get; set; }
}


