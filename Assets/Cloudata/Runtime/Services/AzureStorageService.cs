using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Services
{
    public class AzureStorageService
    { 
        // TODO: move to settings data object
        private const string ConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=thomasthecloud;AccountKey=Tr7iQz03/PJLDiEEg9Dr5JfryUnZc8KEvJPOoHtXCPlmF+87MqenkmAcPvq/Kss+YC7jQzSxebEiixfAC2CG/A==;EndpointSuffix=core.windows.net";
        private const string ContainerName = "container";

        private const string Message =
            "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid";

        public AzureStorageService(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }
        
        public AzureStorageService()
        {
            _connectionString = ConnectionString;
            _containerName = ContainerName;
        }

        private CloudStorageAccount Account
        {
            get
            {
                if (_account == null)
                {
                    try
                    {
                        _account = CloudStorageAccount.Parse(_connectionString);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(Message);
                    }
                }

                return _account;
            }
        }

        private readonly string _connectionString;
        private readonly string _containerName;
        private CloudStorageAccount _account;

        public async Task<CloudBlockBlob> UploadFromMemoryStreamAsync(MemoryStream stream,
            string blobName,
            string contentType = "")
        {
            var client = Account.CreateCloudBlobClient();
            var container = client.GetContainerReference(_containerName);

            await ValidateContainerAsync(container);

            try
            {
                var blob = container.GetBlockBlobReference(blobName);
                blob.Properties.ContentType = contentType;

                var data = stream.ToArray();

                await blob.UploadFromByteArrayAsync(data, 0, data.Length);

                return blob;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task CreateContainerIfNotExistsAsync(string containerName)
        {
            // var account = CloudStorageAccount.Parse(ConnectionString);
            var client = Account.CreateCloudBlobClient();

            var container = client.GetContainerReference(containerName);
            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task ValidateContainerAsync(CloudBlobContainer container)
        {
            if (container.Exists()) return;

            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task DeleteContainerAsync()
        {
            var client = Account.CreateCloudBlobClient();

            var containerName = "container";
            var container = client.GetContainerReference(containerName);
            try
            {
                await container.DeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO: move to Cloudata Manager
        public async Task<CloudBlockBlob> UploadTextureAsync(Texture2D texture, string blobName)
        {
            var client = Account.CreateCloudBlobClient();
            var container = client.GetContainerReference(_containerName);

            await ValidateContainerAsync(container);

            var blob = container.GetBlockBlobReference(blobName);
            blob.Properties.ContentEncoding = "image/png";

            byte[] imageData;

            if (!texture.isReadable)
            {
                var decompressedTex = DecompressTexture(texture, RenderTextureFormat.ARGB32);
                imageData = decompressedTex.EncodeToPNG();
            }
            else
            {
                imageData = texture.EncodeToPNG();
            }

            try
            {
                await blob.UploadFromByteArrayAsync(imageData, 0, imageData.Length);
                return blob;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DownloadTextureAsync(string blobName, string path)
        {
            var client = Account.CreateCloudBlobClient();
            var container = client.GetContainerReference(_containerName);

            await ValidateContainerAsync(container);

            var blob = container.GetBlockBlobReference(blobName);

            try
            {
                await blob.DownloadToFileAsync(path, FileMode.Create);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private Texture2D DecompressTexture(Texture2D source, RenderTextureFormat format)
        {
            var renderTex = RenderTexture.GetTemporary(source.width, source.height, 0, format,
                RenderTextureReadWrite.sRGB);

            Graphics.Blit(source, renderTex);

            var previous = RenderTexture.active;
            RenderTexture.active = renderTex;

            var readableTex = new Texture2D(source.width, source.height);
            readableTex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
            readableTex.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);

            return readableTex;
        }

        #region Unity Callbacks

        #endregion
    }
}