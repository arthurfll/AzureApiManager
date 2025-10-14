using Source.Models;
using Source.Repositories;

namespace Source.Services;

public class DriveFileService
{
  private readonly DriveFileRepository _driveFileRepository;
  private readonly BlobStorageService _blobStorageService;

  public DriveFileService(DriveFileRepository driveFileRepository, BlobStorageService blobStorageService)
  {
    _driveFileRepository = driveFileRepository;
    _blobStorageService = blobStorageService;
  }

  public void AddFile(DriveFile obj)
  {
//    string blobUri = _blobStorageService.UploadFile();
  }
}


