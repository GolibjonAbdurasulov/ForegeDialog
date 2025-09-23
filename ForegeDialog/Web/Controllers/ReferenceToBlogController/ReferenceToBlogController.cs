using DatabaseBroker.Repositories.OurCategoriesRepository;
using DatabaseBroker.Repositories.ReferenceToBlogRepository;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.ReferenceToBlogController.Dtos;

namespace Web.Controllers.ReferenceToBlogController;


[ApiController]
[Route("[controller]/[action]")]
public class ReferenceToBlogController(
    IOurCategoriesRepository ourCategoriesRepository,
    IReferenceToBlogRepository referenceModelRepository)
    : ControllerBase
{
    private IOurCategoriesRepository OurCategoriesRepository { get; set; } = ourCategoriesRepository;
    private IReferenceToBlogRepository ReferenceToBlogRepository { get; set; } = referenceModelRepository;

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ReferenceToBlogCreationDto dto)
    {
        var entity = new ReferenceToBlog()
        {
            CategoryId = dto.CategoryId,
            Categories = null,
            BlogId = dto.BlogId,
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
        var resEntity=await ReferenceToBlogRepository.AddAsync(entity);

        var resDto = new ReferenceToBlogDto
        {
            Id = resEntity.Id,
            CategoryId = resEntity.CategoryId,
            BlogId = resEntity.BlogId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ReferenceToBlogDto dto)
    {
        var res =  await ReferenceToBlogRepository.GetByIdAsync(dto.Id);

        res.CategoryId = dto.CategoryId;
        res.BlogId = dto.BlogId;
      
        await ReferenceToBlogRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ReferenceToBlogRepository.GetByIdAsync(id);
        await ReferenceToBlogRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ReferenceToBlogRepository.GetByIdAsync(id);

        var dto=new ReferenceToBlog()
        {
            Id = res.Id,
            CategoryId = res.CategoryId,
            BlogId = res.BlogId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetReferencesByCategoryIdsAsync(long id)
    {
        var res =   ReferenceToBlogRepository.GetAllAsQueryable().
            Where(item=>item.CategoryId==id).ToList();

        if (res == null)
            throw new NotFoundException("ReferenceModel not found");
        
        List<ReferenceToBlogDto> resDtos = new List<ReferenceToBlogDto>();
        foreach (ReferenceToBlog model in res)
        {
            resDtos.Add(new ReferenceToBlogDto()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                BlogId = model.BlogId,
            });
        }
        return new ResponseModelBase(resDtos);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ReferenceToBlogRepository.GetAllAsQueryable().ToList();

        List<ReferenceToBlogDto> resDto = new List<ReferenceToBlogDto>();
        foreach (ReferenceToBlog model in res)
        {
            resDto.Add(new ReferenceToBlogDto()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                BlogId = model.BlogId,
            });
        }
        return new ResponseModelBase(resDto);
    }
}