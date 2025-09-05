using DatabaseBroker.Repositories.OurServiceRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.OurServicesController.OurServicesDtos;

namespace Web.Controllers.OurServicesController;
[ApiController]
[Route("[controller]/[action]")]
public class OurServicesController : ControllerBase
{
 public IOurServiceRepository OurServiceRepository { get; set; }

    public OurServicesController(IOurServiceRepository ourServiceRepository)
    {
        this.OurServiceRepository = ourServiceRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OurServicesCreationDto dto)
    {
        var entity = new OurService()
        {
            Title = dto.Title,
            Description = dto.Description,
            PicturesId = dto.PicturesId,
        };
        var resEntity=await OurServiceRepository.AddAsync(entity);
        
        var resDto = new OurServicesDto()
        {
            Id = resEntity.Id,
            Title = resEntity.Title,
            Description = resEntity.Description,
            PicturesId = resEntity.PicturesId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OurServicesDto dto)
    {
        var res =  await OurServiceRepository.GetByIdAsync(dto.Id);
        res.Description = dto.Description;
        res.Title=dto.Title;
        res.PicturesId=dto.PicturesId;
      
        await OurServiceRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OurServiceRepository.GetByIdAsync(id);
        await OurServiceRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await OurServiceRepository.GetByIdAsync(id);
        var dto = new OurServicesDto
        {
            Id = res.Id,
            Title = res.Title,
            Description = res.Description,
            PicturesId = res.PicturesId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OurServiceRepository.GetAllAsQueryable().ToList();
        List<OurServicesDto> dtos = new List<OurServicesDto>();
        foreach (OurService question in res)
        {
            dtos.Add(new OurServicesDto
            {
                Id = question.Id,
                Title = question.Title,
                Description = question.Description,
                PicturesId = question.PicturesId,
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}