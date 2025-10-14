using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Source.Services;

public class BlobStorageCredentials
{
  public string AccountKey { get; set; } = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
  public string AccountName { get; set; } = "devstoreaccount1";
  public string BlobStorageEndPoint { get; set; } = "http://127.0.0.1:10000";
}

public class BlobStorageService
{
  public BlobServiceClient GetBlobServiceClient()
  {
    var credentials = new BlobStorageCredentials();

    StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(credentials.AccountName, credentials.AccountKey);

    string blobUri = $"{credentials.BlobStorageEndPoint}/{credentials.AccountName}";
    //  string blobUri = $"https://{credentials.AccountName}.blob.core.windows.net";

    var blobServiceClient = new BlobServiceClient
        (new Uri(blobUri), sharedKeyCredential);

    return blobServiceClient;
  }
    public BlobContainerClient GetBlobContainerClient(
        string containerName)
    {
        var credentials = new BlobStorageCredentials();

        var sharedKeyCredential = new StorageSharedKeyCredential(
            credentials.AccountName,
            credentials.AccountKey
        );

        string blobUri = $"{credentials.BlobStorageEndPoint}/{credentials.AccountName}";

        var blobServiceClient = new BlobServiceClient(
            new Uri(blobUri),
            sharedKeyCredential
        );

        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        blobContainerClient.CreateIfNotExists();

        return blobContainerClient;
    }
public async Task<string> CreateContainerIfNotExistsAsync(BlobServiceClient client)
{
    string containerName = "drive-files";

    try
    {
        // Tenta obter o container
        var containerClient = client.GetBlobContainerClient(containerName);

        // Cria apenas se não existir
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);
     return containerName;
    }
    catch (RequestFailedException e)
    {
        Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
        Console.WriteLine(e.Message);
        throw;
    }
}


  public async Task<string> UploadFile(
    string userId, string folderId, string fileId, BlobContainerClient client , IFormFile formFile
  )
  {
    string blobPath = $"{userId}/{folderId}/{fileId}/{formFile.FileName}";

    BlobClient blobClient = client.GetBlobClient(blobPath);

    await using Stream fileStream = formFile.OpenReadStream();

    await blobClient.UploadAsync(fileStream, overwrite: true);

    Console.WriteLine("Objeto Salvo");

    return blobClient.Uri.ToString();


  }
}

