using BobrVerse.Common.Models.DTO.File;

namespace BobrVerse.Bll.Interfaces
{
    public interface IAzureBlobStorageService
    {
        Task<FileDto> AddFileToBlobStorage(NewFileDto newFileDto);
        Task DeleteFromBlob(FileDto fileDto);
    }
}