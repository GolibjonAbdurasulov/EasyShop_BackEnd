using System;
using System.IO;
using System.Threading.Tasks;
using Entity.Models.File;
using Entity.Models.Product;
using Microsoft.AspNetCore.Http;
namespace Services.Interfaces;

public interface IFileService
{
    ValueTask<FileModel> UploadFileAsync(IFormFile file);
    ValueTask<FileModel> DeleteAsync(Guid id);
    ValueTask<FileModel> UpdateFileAsync(Guid id, IFormFile file);
    ValueTask<string> MakeFilePath(string filePath);
    public string DetectionFileType(string fileName);
    public Task SaveFileAsync(byte[] bytes, string filePath);
    public  ValueTask<FileModel> GetByIdAsync(Guid id);
    public  Task<Stream> SendFileAsync(Guid id);
    public Task<Stream> GetProductCheck(long productId);
    public Task<Product> GetProductsDates(string productType, long productId);
}
