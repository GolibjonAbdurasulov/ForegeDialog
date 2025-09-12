using DatabaseBroker.Repositories.FileRepository;
using DatabaseBroker.Repositories.ResourceRepository;
using Entity.Models;
using Entity.Models.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.Common;
using Web.Controllers.ResourceController.ResourceDtos;

namespace Web.Controllers.ResourceController;

[ApiController]
[Route("[controller]/[action]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public ResourceController(IResourceRepository resourceRepository, IFileRepository fileRepository, IFileService fileService)
    {
        _resourceRepository = resourceRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync(ResourceDto dto)
    {
        var entity = new Resources
        {
            FileName = dto.FileName,
            FileType = dto.FileType,
            Subject = dto.Subject,
            PublishedDate = dto.PublishedDate,
            Size = dto.Size,
            FileIdUZ = dto.FileIdUZ,
            FileIdRU = dto.FileIdRU,
            FileIdEN = dto.FileIdEN,
            FileIdGER = dto.FileIdGER,
            ResourceCategoryId = dto.ResourceCategoryId,
        };

        var resEntity = await _resourceRepository.AddAsync(entity);

      
        resEntity.Size.uz = await GetResourceSizeAsync(resEntity.FileIdUZ);
        resEntity.Size.en = await GetResourceSizeAsync(resEntity.FileIdEN);
        resEntity.Size.ru = await GetResourceSizeAsync(resEntity.FileIdRU);
        resEntity.Size.ger = await GetResourceSizeAsync(resEntity.FileIdGER);

        await _resourceRepository.UpdateAsync(resEntity);

        var resDto = new ResourceDto
        {
            Id = resEntity.Id,
            FileName = resEntity.FileName,
            FileType = resEntity.FileType,
            Subject = resEntity.Subject,
            PublishedDate = resEntity.PublishedDate,
            Size = resEntity.Size,
            FileIdUZ = resEntity.FileIdUZ,
            FileIdRU = resEntity.FileIdRU,
            FileIdEN = resEntity.FileIdEN,
            FileIdGER = resEntity.FileIdGER,
            ResourceCategoryId = resEntity.ResourceCategoryId,
            
        };

        return new ResponseModelBase(resDto);
    }

    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync(ResourceDto dto)
    {
        var res = await _resourceRepository.GetByIdAsync(dto.Id);
        if (res == null) return new ResponseModelBase("Resource topilmadi", System.Net.HttpStatusCode.NotFound);

        res.FileName = dto.FileName;
        res.FileType = dto.FileType;
        res.Subject = dto.Subject;
        res.PublishedDate = dto.PublishedDate;
        res.Size = dto.Size;
        res.FileIdUZ = dto.FileIdUZ;
        res.FileIdRU = dto.FileIdRU;
        res.FileIdEN = dto.FileIdEN;
        res.FileIdGER = dto.FileIdGER;
        res.ResourceCategoryId = dto.ResourceCategoryId;

        await _resourceRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }

    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        var res = await _resourceRepository.GetByIdAsync(id);
        if (res == null) return new ResponseModelBase("Resource topilmadi", System.Net.HttpStatusCode.NotFound);

        await _resourceRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }

    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res = await _resourceRepository.GetByIdAsync(id);
        if (res == null) return new ResponseModelBase("Resource topilmadi", System.Net.HttpStatusCode.NotFound);

        var dto = new ResourceDto
        {
            Id = res.Id,
            FileName = res.FileName,
            FileType = res.FileType,
            Subject = res.Subject,
            PublishedDate = res.PublishedDate,
            Size = res.Size,
            FileIdUZ = res.FileIdUZ,
            FileIdRU = res.FileIdRU,
            FileIdEN = res.FileIdEN,
            FileIdGER = res.FileIdGER,
            ResourceCategoryId = res.ResourceCategoryId
        };
        return new ResponseModelBase(dto);
    }

    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var resList = _resourceRepository.GetAllAsQueryable().ToList();
        var dtos = resList.Select(q => new ResourceDto
        {
            Id = q.Id,
            FileName = q.FileName,
            FileType = q.FileType,
            Subject = q.Subject,
            PublishedDate = q.PublishedDate,
            Size = q.Size,
            FileIdUZ = q.FileIdUZ,
            FileIdRU = q.FileIdRU,
            FileIdEN = q.FileIdEN,
            FileIdGER = q.FileIdGER,
            ResourceCategoryId = q.ResourceCategoryId
        }).ToList();

        return new ResponseModelBase(dtos);
    }

    
    private async Task<string> GetResourceSizeAsync(Guid id)
    {
        await using var stream = await _fileService.SendFileAsync(id); // Stream orqali o‘qish
        long totalBytes = 0;
        var buffer = new byte[81920]; // 80 KB buffer

        int read;
        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            totalBytes += read;
        }

        return FormatSize(totalBytes);
    }

    private string FormatSize(long bytes)
    {
        const double KB = 1024;
        const double MB = KB * 1024;
        const double GB = MB * 1024;

        if (bytes >= GB)
            return $"{bytes / GB:0.##} GB";
        if (bytes >= MB)
            return $"{bytes / MB:0.##} MB";
        if (bytes >= KB)
            return $"{bytes / KB:0.##} KB";
        return $"{bytes} B";
    }
}
