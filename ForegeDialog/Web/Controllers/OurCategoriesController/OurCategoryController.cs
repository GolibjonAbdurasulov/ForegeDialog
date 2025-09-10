using DatabaseBroker.Repositories.OurCategoriesRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.OurCategoriesController.OurCategoriesDtos;
using Web.Controllers.OurTeamController.OurTeamDtos;

namespace Web.Controllers.OurCategoriesController;

[ApiController]
[Route("[controller]/[action]")]
public class OurCategoryController  : ControllerBase
{
    public IOurCategoriesRepository OurCategoriesRepository { get; set; }

    public OurCategoryController(IOurCategoriesRepository ourCategoriesRepository)
    {
        this.OurCategoriesRepository = ourCategoriesRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OurCategoriesCreationDto dto)
    {
        var entity = new OurCategories
        {
            Name = dto.Name,
            PicturesId = dto.PicturesId,
            
        };
        var resEntity=await OurCategoriesRepository.AddAsync(entity);
        
        var resDto = new OurCategoriesDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            PicturesId = resEntity.PicturesId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OurCategoriesDto dto)
    {
        var res =  await OurCategoriesRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.PicturesId = dto.PicturesId;
      
        await OurCategoriesRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OurCategoriesRepository.GetByIdAsync(id);
        await OurCategoriesRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await OurCategoriesRepository.GetByIdAsync(id);
        var dto = new OurCategories()
        {
            Id = res.Id,
            Name = res.Name,
            PicturesId = res.PicturesId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OurCategoriesRepository.GetAllAsQueryable().ToList();
        List<OurCategoriesDto> dtos = new List<OurCategoriesDto>();
        foreach (OurCategories question in res)
        {
            dtos.Add(new OurCategoriesDto()
            {
                Id = question.Id,
                Name = question.Name,
                PicturesId = question.PicturesId,
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}