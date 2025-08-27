using DatabaseBroker.Repositories.TagsRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.NewsCategoryController.NewsCategoryDto;

namespace Web.Controllers.TagsController;
[ApiController]
[Route("[controller]/[action]")]
public class TagsController : ControllerBase
{
    private ITagsRepository TagsRepository { get; set; }

    public TagsController(ITagsRepository newsCategoryRespository)
    {
        this.TagsRepository = newsCategoryRespository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( Tags dto)
    {
        var entity = new Tags
        {
            TagName = dto.TagName,
        };
        var resEntity=await TagsRepository.AddAsync(entity);
        
        var resDto = new Tags()
        {
            Id = resEntity.Id,
            TagName = resEntity.TagName,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( NewsCategory dto)
    {
        var res =  await TagsRepository.GetByIdAsync(dto.Id);

        res.TagName = dto.CategoryName;
      
        await TagsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await TagsRepository.GetByIdAsync(id);
        await TagsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await TagsRepository.GetByIdAsync(id);

        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   TagsRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    }  
}