using Source.Data;

namespace Source.Repositories;

public class DriveFileRepository
{
  private readonly AppDbContext _db;

  public DriveFileRepository(AppDbContext db)
  {
    _db = db;
  }
}

