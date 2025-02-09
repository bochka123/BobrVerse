namespace BobrVerse.Common.Models.DTO.File
{
    public class NewFileDto
    {
        public Stream Stream { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}