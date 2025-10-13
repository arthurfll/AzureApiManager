using Source.Repositories;

namespace Source.Services;

public class DriveFileService
{
  private readonly DriveFileRepository _driveFileRepository;

  public DriveFileService(DriveFileRepository driveFileRepository)
  {
    _driveFileRepository = driveFileRepository;
  }
}

