using DatabaseBroker.Repositories.ImageModelRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.ResourceCategoryController.ResourceCategoryDto;

namespace Web.Controllers.ImageModelConrtoller;

[ApiController]
[Route("[controller]/[action]")]
public class ImageModelController : ControllerBase
{
    private IImageModelRepository ImageModelRepository { get; set; }

    public ImageModelController(IImageModelRepository respository)
    {
        this.ImageModelRepository = respository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ImageModel dto)
    {
        var entity = new ImageModel
        {
            FileId = dto.FileId,
            ImageName = dto.ImageName,
        };
        var resEntity=await ImageModelRepository.AddAsync(entity);
        
        return new ResponseModelBase(resEntity);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ImageModel dto)
    {
        var res =  await ImageModelRepository.GetByIdAsync(dto.Id);

        res.FileId=dto.FileId;
        res.ImageName=dto.ImageName;
      
        await ImageModelRepository.UpdateAsync(res);
        return new ResponseModelBase(res);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ImageModelRepository.GetByIdAsync(id);
        await ImageModelRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ImageModelRepository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ImageModelRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    } 
}