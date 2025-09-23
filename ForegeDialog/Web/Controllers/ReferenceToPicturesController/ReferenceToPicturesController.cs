using DatabaseBroker.Repositories.OurCategoriesRepository;
using DatabaseBroker.Repositories.ReferenceModelRepository;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.ReferenceToPicturesController.Dtos;

namespace Web.Controllers.ReferenceToPicturesController;

[ApiController]
[Route("[controller]/[action]")]
public class ReferenceToPicturesController(
    IOurCategoriesRepository ourCategoriesRepository,
    IReferenceModelRepository referenceModelRepository)
    : ControllerBase
{
    public IOurCategoriesRepository OurCategoriesRepository { get; set; } = ourCategoriesRepository;
    public IReferenceModelRepository ReferenceModelRepository { get; set; } = referenceModelRepository;

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ReferenceModelCreationDto dto)
    {
        var entity = new ReferenceModel
        {
            CategoryId = dto.CategoryId,
            Categories = null,
            PicturesModelId = dto.PicturesModelId,
        };

        try
        {
            entity.Categories = await OurCategoriesRepository.GetByIdAsync(dto.CategoryId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Our categories couldn't be found");
            throw;
        }
        var resEntity=await ReferenceModelRepository.AddAsync(entity);
        
        return new ResponseModelBase(resEntity);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ReferenceModelDto dto)
    {
        var res =  await ReferenceModelRepository.GetByIdAsync(dto.Id);

        res.CategoryId = dto.CategoryId;
        res.PicturesModelId = dto.PicturesModelId;
      
        await ReferenceModelRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ReferenceModelRepository.GetByIdAsync(id);
        await ReferenceModelRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ReferenceModelRepository.GetByIdAsync(id);

        var dto=new ReferenceModelDto
        {
            Id = res.Id,
            CategoryId = res.CategoryId,
            PicturesModelId = res.PicturesModelId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetReferencesByCategoryIdsAsync(long id)
    {
        var res =   ReferenceModelRepository.GetAllAsQueryable().
            Where(item=>item.CategoryId==id).ToList();

        if (res == null)
            throw new NotFoundException("ReferenceModel not found");
        
        List<ReferenceModelDto> resDto = new List<ReferenceModelDto>();
        foreach (ReferenceModel model in res)
        {
            resDto.Add(new ReferenceModelDto
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                PicturesModelId = model.PicturesModelId,
            });
        }
        return new ResponseModelBase(resDto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ReferenceModelRepository.GetAllAsQueryable().ToList();

        List<ReferenceModelDto> resDto = new List<ReferenceModelDto>();
        foreach (ReferenceModel model in res)
        {
            resDto.Add(new ReferenceModelDto
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                PicturesModelId = model.PicturesModelId,
            });
        }
        return new ResponseModelBase(resDto);
    }
}