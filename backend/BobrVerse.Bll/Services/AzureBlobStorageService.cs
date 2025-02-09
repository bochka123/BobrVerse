using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Helpers;
using BobrVerse.Common.Models.DTO.File;
using Microsoft.AspNetCore.StaticFiles;

namespace BobrVerse.Bll.Services
{
    public class AzureBlobStorageService(BlobContainerOptionsHelper blobContainerOptionsHelper, BlobServiceClient blobServiceClient) : IAzureBlobStorageService
    {
        public async Task<FileDto> AddFileToBlobStorage(NewFileDto newFileDto)
        {
            if (newFileDto.Stream == null)
            {
                throw new ArgumentNullException($"{newFileDto.FileName} is empty");
            }

            await CreateDirectory(blobContainerOptionsHelper.BlobContainerName);

            var uniqueFileName = CreateName(newFileDto.FileName, blobContainerOptionsHelper.BlobContainerName);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(newFileDto.FileName, out string contentType))
            {
                throw new ArgumentNullException($"{newFileDto.FileName} can not get content type");
            }

            var blob = blobServiceClient.GetBlobContainerClient(blobContainerOptionsHelper.BlobContainerName)
                    .GetBlobClient(uniqueFileName);

            var blobHttpHeaders = new BlobHttpHeaders();
            blobHttpHeaders.ContentType = contentType;

            await blob.UploadAsync(newFileDto.Stream, blobHttpHeaders);

            var uploadFile = new FileDto()
            {
                Url = blob.Uri.AbsoluteUri
            };

            return uploadFile;
        }

        public async Task DeleteFromBlob(FileDto file)
        {
            var fileName = Path.GetFileName(file.Url);
            var blob = blobServiceClient.GetBlobContainerClient(blobContainerOptionsHelper.BlobContainerName)
                .GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        private async Task CreateDirectory(string folderPath)
        {
            if (!blobServiceClient.GetBlobContainerClient(folderPath).Exists())
            {
                await blobServiceClient.CreateBlobContainerAsync(folderPath);
            }
        }


        private string CreateName(string fileName, string folderPath)
        {
            var blob = blobServiceClient.GetBlobContainerClient(folderPath).GetBlobClient(fileName);

            if (blob.Exists())
            {
                return $"{Path.GetFileNameWithoutExtension(fileName)}_" +
                    $"{Guid.NewGuid()}" +
                    $"{Path.GetExtension(fileName)}";
            }

            return fileName;
        }   
    }
}