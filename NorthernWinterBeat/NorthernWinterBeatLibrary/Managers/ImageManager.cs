using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public class ImageManager : IImageManager


    {
        private BlobContainerClient container { get; set; }

        private string cName = "";

        public string ContainerURL {
            get {
                return "https://nwbimages.blob.core.windows.net/" + cName;
            }
        }

        public ImageManager(string containerName, string connectionString)
        {
            cName = containerName;
            container = new BlobContainerClient(connectionString, cName);
            container.CreateIfNotExists();

            Console.WriteLine("IN BLOB");

        }

        public void Upload(string name, Stream file)
        {
            var curBlob = container.GetBlobClient(name);
            if (!curBlob.Exists())
            {
                container.UploadBlob(name, file);
            }
        }
    }
}
