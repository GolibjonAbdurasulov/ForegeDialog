using DatabaseBroker.Repositories.ResourceCategoryRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.ResourceCategoryController.ResourceCategoryDto;

namespace Web.Controllers.ResourceCategoryController;

[ApiController]
[Route("[controller]/[action]")]
public class ResourceCategoryController : ControllerBase
{
    private IResourceCategoryRepository ResourceCategoryRepository { get; set; }

    public ResourceCategoryController(IResourceCategoryRepository respository)
    {
        this.ResourceCategoryRepository = respository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ResourceCategoryCreationDto dto)
    {
        var entity = new ResourceCategory()
        {
            CategoryName = dto.CategoryName,
        };
        var resEntity=await ResourceCategoryRepository.AddAsync(entity);
        
        var resDto = new ResourceCategory()
        {
            Id = resEntity.Id,
            CategoryName = resEntity.CategoryName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ResourceCategory dto)
    {
        var res =  await ResourceCategoryRepository.GetByIdAsync(dto.Id);

        res.CategoryName = dto.CategoryName;
      
        await ResourceCategoryRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ResourceCategoryRepository.GetByIdAsync(id);
        await ResourceCategoryRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ResourceCategoryRepository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ResourceCategoryRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    } 
}