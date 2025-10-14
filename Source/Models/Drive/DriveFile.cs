using System.ComponentModel.DataAnnotations;

namespace Source.Models;

public class DriveFile
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  public Guid UserId { get; set; }

  [Required]
  public Guid FolderId { get; set; }
  public DriveFolder? Folder { get; set; }
  
  [Required]
  public string Name { get; set; } = "";
  [Required]
  public string FileUrl { get; set; } = "";
}


