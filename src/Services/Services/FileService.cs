using System;
using System.IO;
using System.Threading.Tasks;
using DatabaseBroker.Repositories.FileRepository;
using Entity.Attributes;
using Entity.Models.File;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Services.Services;

[Injectable]
public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    //private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }


    public async ValueTask<FileModel> UploadFileAsync(IFormFile file)
    {
        var resultFile = await CreateAsync(file);
        await SaveFileAsync(file, resultFile.Path);
        return resultFile;
    }

    // private async Task SaveFileAsync(IFormFile file, string filePath)
    // {
    //     using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
    //     {
    //         await file.CopyToAsync(stream);
    //     }
    // }
    public async Task SaveFileAsync(IFormFile file, string filePath)
    {
        // Fayl yo'lining katalog qismini olish
        var directory = Path.GetDirectoryName(filePath);

        // Agar katalog mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    // public async Task SaveFileAsync(byte[] bytes,string filePath)
    // {
    //     using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
    //     {
    //         stream.Write(bytes);
    //     }
    // }

    public async Task SaveFileAsync(byte[] bytes, string filePath)
    {
        await File.WriteAllBytesAsync(filePath, bytes);
    }

    public async ValueTask<string> MakeFilePath(string filePath)
    {
        var webRootFolder =  "wwwroot/uploads";
        var path = Path.Combine(Directory.GetCurrentDirectory(), webRootFolder, filePath);
        return path;
    }

    private async Task<FileModel> CreateAsync(IFormFile file)
    {
        var id = Guid.NewGuid();
        string fileName = id.ToString() + DetectionFileType(file.FileName);
        var resultFile = new FileModel()
        {
            Id = id,
            FileName = fileName,
            ContentType = file.ContentType ?? string.Empty,
            Path = await MakeFilePath(fileName)
        };
        resultFile = await _fileRepository.AddAsync(resultFile);
        return resultFile;
    }

    public string DetectionFileType(string fileName)
    {
        string[] names = fileName.Split('.');
        return ("." + names[names.Length - 1]);
    }


    public async ValueTask<FileModel> DeleteAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file is null)
            throw new Exception("File not found");
        await RemoveFileWebRootFolderAsync(file.Path);
        var result = await _fileRepository.RemoveAsync(file);
        return result;
    }

    private async Task RemoveFileWebRootFolderAsync(string path)
    {
        if (System.IO.File.Exists(path))
        {
            await Task.Run(() => System.IO.File.Delete(path));
        }
    }


    public async ValueTask<FileModel> UpdateFileAsync(Guid id, IFormFile file)
    {
        var updateFile = await GetByIdAsync(id);
        updateFile = await UpdateAsync(updateFile, file);
        await RemoveFileWebRootFolderAsync(updateFile.Path);
        await SaveFileAsync(file, updateFile.Path);
        return updateFile;
    }

    private async ValueTask<FileModel> UpdateAsync(FileModel file,
        IFormFile updateFile)
    {
        file.FileName = updateFile.FileName;
        file.ContentType = updateFile.ContentType;
        file = await _fileRepository.UpdateAsync(file);
        return file;
    }

    // public async ValueTask<(FileStream, FileModel)> GetByFileIdAsync(long id)
    // {
    //     var file = await GetByIdAsync(id);
    //     var fileStream = new FileStream(file.Path, FileMode.Open, FileAccess.Read);
    //
    //     return (fileStream, file);
    // }

    public async ValueTask<FileModel> GetByIdAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file is null)
            throw new Exception("File not  found");
        return file;
    }

    public async Task<Stream> SendFileAsync(Guid id)
    {
        var file = await _fileRepository.GetByIdAsync(id);
        if (file == null || !System.IO.File.Exists(file.Path))
            throw new FileNotFoundException("Fayl topilmadi.");

        var stream = new FileStream(file.Path, FileMode.Open, FileAccess.Read);
        return stream;
    }


}