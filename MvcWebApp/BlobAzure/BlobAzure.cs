using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MvcWebApp.BlobAzure
{
    public static class BlobAzure
    {
        //private readonly Ap _context;
        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=atazureblob;AccountKey=Tc+UgImVcNe7jPaF4opQRI4/eu6w/qgYh1Gj56EOlBnem5188abu5kjQjUFNCyHb5pVUFiQWGUMd+AStUHS3KQ==;EndpointSuffix=core.windows.net";
        private const string containerName = "images";

        public static async Task<string> UploadImage(IFormFile imageFile)
        {

            var reader = imageFile.OpenReadStream();
            var cloundStorageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = cloundStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            string path = Guid.NewGuid().ToString();
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            await blob.UploadFromStreamAsync(reader);
            return blob.Uri.ToString();
        }

        public static void DeletePhoto(string Url)
        {
            if (Url != null)
            {
                try
                {
                    string nomeArquivo = Url.Split("/" + containerName + "/")[1];
                    var blobClient = new BlobClient(connectionString, containerName, nomeArquivo);
                    blobClient.Delete();
                }
                catch { }
            }
        }

    }
}
