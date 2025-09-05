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
     public IResourceRepository ResourceRepository { get; set; }
     public IFileService FileService { get; set; }

    public ResourceController(IResourceRepository resourceRepository)
    {
        this.ResourceRepository = resourceRepository;
    }

   

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ResourceDto dto)
    {
        var entity = new Resource
        {
            FileName = dto.FileName,
            FileType = dto.FileType,
            Subject = dto.Subject,
            PublishedDate = dto.PublishedDate,
            Size = dto.Size,
            FileId = dto.FileId,
        };
        
        var resEntity=await ResourceRepository.AddAsync(entity);
        resEntity.File = await FileService.GetByIdAsync(dto.FileId);
        resEntity.FileType = resEntity.File.ContentType.ToString();
        resEntity.Size = await GetResourceSize(resEntity.File.Id);
        await ResourceRepository.UpdateAsync(resEntity);
        
        var resDto = new ResourceDto()
        {
            Id=resEntity.Id,   
            FileName = resEntity.FileName,
            FileType = resEntity.FileType,
            Subject = resEntity.Subject,
            PublishedDate = resEntity.PublishedDate,
            Size = resEntity.Size,
            FileId = resEntity.FileId,
            
        };
        return new ResponseModelBase(resDto);
    }
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ResourceDto dto)
    {
        var res =  await ResourceRepository.GetByIdAsync(dto.Id);
        res.FileName = dto.FileName;
        res.FileType = dto.FileType;
        res.Subject = dto.Subject;
        res.PublishedDate = dto.PublishedDate;
        res.Size = dto.Size;
        res.FileId = dto.FileId;
      
        await ResourceRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ResourceRepository.GetByIdAsync(id);
        await ResourceRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ResourceRepository.GetByIdAsync(id);
        var dto = new ResourceDto()
        {
            Id = res.Id,
            FileName = res.FileName,
            FileType = res.FileType,
            Subject = res.Subject,
            PublishedDate = res.PublishedDate,
            Size = res.Size,
            FileId = res.FileId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ResourceRepository.GetAllAsQueryable().ToList();
        List<ResourceDto> dtos = new List<ResourceDto>();
        foreach (Resource question in res)
        {
            dtos.Add(new ResourceDto
            {
                Id = question.Id,
                FileName = question.FileName,
                FileType = question.FileType,
                Subject = question.Subject,
                PublishedDate = question.PublishedDate,
                Size = question.Size,
                FileId = question.FileId,
            });
        }
        
        return new ResponseModelBase(dtos);
    }

    private async Task<string> GetResourceSize(Guid id)
    {
        using var res = await FileService.SendFileAsync(id);

        long totalBytes = 0;
        byte[] buffer = new byte[81920]; 
        int read;

        while ((read = await res.ReadAsync(buffer, 0, buffer.Length)) > 0)
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
            return $"{bytes / GB:0.##} GB";   // 2 raqamgacha aniqlik
        else if (bytes >= MB)
            return $"{bytes / MB:0.##} MB";
        else if (bytes >= KB)
            return $"{bytes / KB:0.##} KB";
        else
            return $"{bytes} B";
    }

    
}