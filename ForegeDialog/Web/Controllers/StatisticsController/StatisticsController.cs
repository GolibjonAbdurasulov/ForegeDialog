using DatabaseBroker.Repositories.StatisticsRepository;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.OurServicesController.OurServicesDtos;
using Web.Controllers.StatisticsController.StatisticsDtos;

namespace Web.Controllers.StatisticsController;

[ApiController]
[Route("[controller]/[action]")]
public class StatisticsController : ControllerBase
{
     public IStatisticsRepository StatisticsRepository { get; set; }

    public StatisticsController(IStatisticsRepository StatisticsRepository)
    {
        this.StatisticsRepository = StatisticsRepository;
    }

   

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( StatisticsCreationDto dto)
    {
        var entity = new Statistics
        {
            HappyClients = dto.HappyClients,
            Projects = dto.Projects,
            TeamMembers = dto.TeamMembers,
            YearsExperience = dto.YearsExperience,
        };
        var resEntity=await StatisticsRepository.AddAsync(entity);
        
        var resDto = new StatisticsDto
        {
            Id = resEntity.Id,
            HappyClients = resEntity.HappyClients,
            Projects = resEntity.Projects,
            TeamMembers = resEntity.TeamMembers,
            YearsExperience = resEntity.YearsExperience,
        };
        return new ResponseModelBase(resDto);
    }
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( StatisticsDto dto)
    {
        var res =  await StatisticsRepository.GetByIdAsync(dto.Id);
        res.Projects = dto.Projects;
        res.HappyClients=dto.HappyClients;
        res.TeamMembers=dto.TeamMembers;
        res.YearsExperience=dto.YearsExperience;
      
        await StatisticsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await StatisticsRepository.GetByIdAsync(id);
        await StatisticsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAsync()
    {
        var res =   StatisticsRepository.GetAllAsQueryable().ToList()[0];
        var dto = new StatisticsDto
        {
            Id = res.Id,
            HappyClients = res.HappyClients,
            Projects = res.Projects,
            TeamMembers = res.TeamMembers,
            YearsExperience = res.YearsExperience   
        };
        return new ResponseModelBase(dto);
    }
    
}