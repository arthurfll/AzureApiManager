using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Source.Models;

public class BlobStorageCredentials
{
  public string AccountKey { get; set; } = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
  public string AccountName { get; set; } = "devstoreaccount1";
  public string BlobStorageEndPoint { get; set; } = "http://127.0.0.1:10000";
}

public class BlobStorageService
{
  public static BlobServiceClient GetBlobServiceClient()
  {
    var credentials = new BlobStorageCredentials();

    StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(credentials.AccountName, credentials.AccountKey);

    string blobUri = $"{credentials.BlobStorageEndPoint}/{credentials.AccountName}";
    //  string blobUri = $"https://{credentials.AccountName}.blob.core.windows.net";

    var blobServiceClient = new BlobServiceClient
        (new Uri(blobUri), sharedKeyCredential);

    return blobServiceClient;
  }
    public static BlobContainerClient GetBlobContainerClient(
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
  public async Task<BlobContainerClient?> CreateContainerIfNotExistsAsync(BlobServiceClient client)
  {
    string containerName = "container-" + Guid.NewGuid();

    try
    {
      await foreach (BlobContainerItem containerItem in client.GetBlobContainersAsync())
      {
        Console.WriteLine("Já existe um container na storage account: {0}", containerItem.Name);
      }

      BlobContainerClient container = await client.CreateBlobContainerAsync(containerName);

      if (await container.ExistsAsync())
      {
        Console.WriteLine("Created container {0}", container.Name);
        return container;
      }
    }
    catch (RequestFailedException e)
    {
      Console.WriteLine("HTTP error code {0}: {1}", e.Status, e.ErrorCode);
      Console.WriteLine(e.Message);
    }

    return null;
  }

  public async Task<string> UploadFile(
    string userId, string folderId, string fileId, BlobContainerClient client , IFormFile formFile
  )
  {
    string blobPath = $"{userId}/{folderId}/{fileId}/{formFile.FileName}";

    BlobClient blobClient = client.GetBlobClient(blobPath);

    await using Stream fileStream = formFile.OpenReadStream();

    await blobClient.UploadAsync(fileStream, overwrite: true);

    return blobClient.Uri.ToString();
  }
}

