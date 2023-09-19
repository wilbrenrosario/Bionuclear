namespace Bionuclear.Core.AzureFiles
{
    public interface IFileShare
    {
        Task FileUploadAsync(FilesDetails fileDetails);
        Task<string> FileDownloadAsync(string fileShareName);
    }
}
