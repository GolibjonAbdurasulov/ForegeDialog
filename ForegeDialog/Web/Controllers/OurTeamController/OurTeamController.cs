using DatabaseBroker.Repositories.OurTeamRepository;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.OurTeamController.OurTeamDtos;

namespace Web.Controllers.OurTeamController;

[ApiController]
[Route("[controller]/[action]")]
public class OurTeamController : ControllerBase
{
    public IOurTeamRepository OurTeamRepository { get; set; }

    public OurTeamController(IOurTeamRepository ourTeamRepository)
    {
        this.OurTeamRepository = ourTeamRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( OurTeamCreationDto dto)
    {
        var entity = new OurTeam
        {
            Name = dto.Name,
            Role = dto.Role,
            About = dto.About,
            Experience = dto.Experience,
            Skills = dto.Skills,
            PicturesId = dto.PicturesId,
        };
        var resEntity=await OurTeamRepository.AddAsync(entity);
        
        var resDto = new OurTeamDto
        {
            Id = resEntity.Id,
            Name = resEntity.Name,
            Role = resEntity.Role,
            About = resEntity.About,
            Experience = resEntity.Experience,
            Skills = resEntity.Skills,
            PicturesId = resEntity.PicturesId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( OurTeamDto dto)
    {
        var res =  await OurTeamRepository.GetByIdAsync(dto.Id);
        res.Name = dto.Name;
        res.Role = dto.Role;
        res.About = dto.About;  
        res.Experience = dto.Experience;
        res.Skills = dto.Skills;
        res.PicturesId = dto.PicturesId;
      
        await OurTeamRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await OurTeamRepository.GetByIdAsync(id);
        await OurTeamRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await OurTeamRepository.GetByIdAsync(id);
        var dto = new OurTeamDto
        {
            Id = res.Id,
            Name = res.Name,
            Role = res.Role,
            About = res.About,
            Experience = res.Experience,
            Skills = res.Skills,
            PicturesId = res.PicturesId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   OurTeamRepository.GetAllAsQueryable().ToList();
        List<OurTeamDto> dtos = new List<OurTeamDto>();
        foreach (OurTeam question in res)
        {
            dtos.Add(new OurTeamDto
            {
                Id = question.Id,
                Name = question.Name,
                Role = question.Role,
                About = question.About,
                Experience = question.Experience,
                Skills = question.Skills,
                PicturesId = question.PicturesId,
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
}