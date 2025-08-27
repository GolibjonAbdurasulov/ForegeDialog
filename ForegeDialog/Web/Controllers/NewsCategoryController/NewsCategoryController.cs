using DatabaseBroker.Repositories.NewsCategoryRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.NewsCategoryController.NewsCategoryDto;

namespace Web.Controllers.NewsCategoryController;

[ApiController]
[Route("[controller]/[action]")]
public class NewsCategoryController : ControllerBase
{
    private INewsCategoryRespository NewsCategoryRespository { get; set; }

    public NewsCategoryController(INewsCategoryRespository newsCategoryRespository)
    {
        this.NewsCategoryRespository = newsCategoryRespository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( NewsCategoryCreationDto dto)
    {
        var entity = new NewsCategory
        {
            CategoryName = dto.CategoryName,
        };
        var resEntity=await NewsCategoryRespository.AddAsync(entity);
        
        var resDto = new NewsCategory()
        {
            Id = resEntity.Id,
            CategoryName = resEntity.CategoryName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( NewsCategory dto)
    {
        var res =  await NewsCategoryRespository.GetByIdAsync(dto.Id);

        res.CategoryName = dto.CategoryName;
      
        await NewsCategoryRespository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await NewsCategoryRespository.GetByIdAsync(id);
        await NewsCategoryRespository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await NewsCategoryRespository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   NewsCategoryRespository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    } 
}