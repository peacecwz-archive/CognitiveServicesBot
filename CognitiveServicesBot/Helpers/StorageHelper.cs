using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveServicesBot
{
    public class StorageHelper
    {
        #region Singleton
        private static StorageHelper _instance;
        public static StorageHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StorageHelper();
                return _instance;
            }
        }
        #endregion

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            }
        }


        CloudStorageAccount storageAccount;
        CloudBlobClient blobClient;
        CloudBlobContainer container;

        public StorageHelper()
        {
            storageAccount = CloudStorageAccount.Parse(ConnectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("chatbotimages");
        }

        public async Task<string> UploadFile(Stream stream, string friendlyUrl,string extension)
        {
            if (await container.CreateIfNotExistsAsync())
                await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(friendlyUrl + "-" + Guid.NewGuid().ToString().Split('-')[0] + extension);

            try
            {
                await blockBlob.UploadFromStreamAsync(stream);
                return blockBlob.Uri.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}