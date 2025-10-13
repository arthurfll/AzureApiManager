using System.ComponentModel.DataAnnotations;

namespace Source.Models;

public class DriveFile
{
  [Key]
  public Guid Id { get; set; }
  [Required]
  public string Name { get; set; } = "";
  [Required]
  public string FileUrl { get; set; } = "";
}

