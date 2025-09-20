using DatabaseBroker.Repositories.PicturesModelRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.ResourceCategoryController.ResourceCategoryDto;

namespace Web.Controllers.PicturesController;

[ApiController]
[Route("[controller]/[action]")]
public class PicturesController : ControllerBase
{
    private IPicturesModelRepository PicturesModelRepository { get; set; }

    public PicturesController(IPicturesModelRepository respository)
    {
        this.PicturesModelRepository = respository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( PicturesModel dto)
    {
        var entity = new PicturesModel
        {
            CategoryId = dto.CategoryId,
            ImageCategory = dto.ImageCategory,
            Images = dto.Images
        };
        var resEntity=await PicturesModelRepository.AddAsync(entity);
        

        return new ResponseModelBase(resEntity);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( PicturesModel dto)
    {
        var res =  await PicturesModelRepository.GetByIdAsync(dto.Id);
        
        res.Images = dto.Images;
        res.CategoryId = dto.CategoryId;
      
        await PicturesModelRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await PicturesModelRepository.GetByIdAsync(id);
        await PicturesModelRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await PicturesModelRepository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   PicturesModelRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    } 
}