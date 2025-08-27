using DatabaseBroker.Repositories.OurPartnersRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.OurPartnersController.OurPartnersDtos;

namespace Web.Controllers.OurPartnersController;

[ApiController]
[Route("[controller]/[action]")]
public class OurPartnersController : ControllerBase
{
    public IOurPartnersRepository OurPartnersRepository { get; set; }

    public OurPartnersController(IOurPartnersRepository ourPartnersRepository)
    {
        this.OurPartnersRepository = ourPartnersRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OurPartnersCreationDto dto)
    {
        var entity = new OurPartners()
        {
            Name = dto.Name,
            About = dto.About,
            Link = dto.Link,
            PicturesId = dto.PicturesId,
        };
        var resEntity=await OurPartnersRepository.AddAsync(entity);
        
        var resDto = new OurPartnersDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            About = resEntity.About,
            Link = resEntity.Link,
            PicturesId = resEntity.PicturesId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OurPartnersDto dto)
    {
        var res =  await OurPartnersRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.About = dto.About;  
        res.Link = dto.Link;
        res.PicturesId = dto.PicturesId;
      
        await OurPartnersRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OurPartnersRepository.GetByIdAsync(id);
        await OurPartnersRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await OurPartnersRepository.GetByIdAsync(id);
        var dto = new OurPartnersDto
        {
            Id = res.Id,
            Name = res.Name,
            About = res.About,
            Link = res.Link,
            PicturesId = res.PicturesId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OurPartnersRepository.GetAllAsQueryable().ToList();
        List<OurPartnersDto> dtos = new List<OurPartnersDto>();
        foreach (OurPartners question in res)
        {
            dtos.Add(new OurPartnersDto()
            {
                Id = question.Id,
                Name = question.Name,
                About = question.About,
                Link = question.Link,
                PicturesId = question.PicturesId,
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}