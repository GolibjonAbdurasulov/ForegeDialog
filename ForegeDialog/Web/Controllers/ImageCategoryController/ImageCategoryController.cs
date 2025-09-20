using DatabaseBroker.Repositories.ImageCategoryRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers.ImageCategoryController;

[ApiController]
[Route("[controller]/[action]")]
public class ImageCategoryController : ControllerBase
{
    private IImageCategoryRepository ImageCategoryRepository { get; set; }

    public ImageCategoryController(IImageCategoryRepository respository)
    {
        this.ImageCategoryRepository = respository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ImageCategory dto)
    {
        var entity = new ImageCategory
        {
            Category =  dto.Category,
        };
        var resEntity=await ImageCategoryRepository.AddAsync(entity);
        

        return new ResponseModelBase(resEntity);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ImageCategory dto)
    {
        var res =  await ImageCategoryRepository.GetByIdAsync(dto.Id);

        res.Category = dto.Category;
      
        await ImageCategoryRepository.UpdateAsync(res);
        return new ResponseModelBase(res);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ImageCategoryRepository.GetByIdAsync(id);
        await ImageCategoryRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ImageCategoryRepository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ImageCategoryRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    } 
}