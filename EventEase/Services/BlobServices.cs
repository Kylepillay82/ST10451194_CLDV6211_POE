using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EventEase.Services
{
    public class BlobService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobService(IConfiguration config)
        {
            var blobStorageSection = config.GetSection("BlobStorage");
            _connectionString = blobStorageSection["ConnectionString"];
            _containerName = blobStorageSection["ContainerName"];
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            // Create the blob container client
            var blobContainerClient = new BlobContainerClient(_connectionString, _containerName);

            // Create container if it does not exist
            await blobContainerClient.CreateIfNotExistsAsync();

            // Optionally, set public access level to allow direct URL access (Blob or Container)
            await blobContainerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // Create a unique name for the blob to avoid overwriting
            var blobName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

            // Get a blob client
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            // Upload the file stream
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            // Return the URI to the uploaded blob
            return blobClient.Uri.ToString();
        }
    }
}
