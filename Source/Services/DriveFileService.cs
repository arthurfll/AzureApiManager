using Azure.Storage.Blobs;
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

  public async Task AddFile(DriveFileCreateDto obj)
  {
    var blobUri = "";
    var blobServiceClient = _blobStorageService.GetBlobServiceClient();

    var containerName = await _blobStorageService.CreateContainerIfNotExistsAsync(blobServiceClient);
    
    Console.WriteLine($"{containerName}");
    try
    {
      var client = _blobStorageService.GetBlobContainerClient(containerName);
      
      blobUri = await _blobStorageService.UploadFile(
          userId: "user123",
          folderId: "folder123",
          fileId: Guid.NewGuid().ToString(),
          client: client,
          formFile: obj.File!
      );
    }
    catch
    { Console.WriteLine("erro"); }



    Console.WriteLine($"Arquivo salvo em: {blobUri}");
  }
}
