using DatabaseBroker.Repositories.PublisherRepository;
using DatabaseBroker.Repositories.StatisticsRepository;
using Entity.Models;
using Entity.Models.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.StatisticsController.StatisticsDtos;

namespace Web.Controllers.PublisherController;
[ApiController]
[Route("[controller]/[action]")]
public class PublisherController : ControllerBase
{
     public IPublisherRepository PublisherRepository { get; set; }

    public PublisherController(IPublisherRepository publicRepository)
    {
        this.PublisherRepository = publicRepository;
    }

   

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( Publisher dto)
    {
        var entity = new Publisher
        {
            Name = dto.Name,
            ImageId = dto.ImageId,
        };
        var resEntity=await PublisherRepository.AddAsync(entity);
        
        return new ResponseModelBase(resEntity);
    }
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( Publisher dto)
    {
        var res =  await PublisherRepository.GetByIdAsync(dto.Id);
        res.Name=dto.Name;
        res.ImageId=dto.ImageId;
      
        await PublisherRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await PublisherRepository.GetByIdAsync(id);
        await PublisherRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await PublisherRepository.GetByIdAsync(id);
        var dto = new Publisher
        {
            Id = res.Id,
            Name = res.Name,
            ImageId = res.ImageId,
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync(long id)
    {
        var res =  PublisherRepository.GetAllAsQueryable().ToList();
        
        return new ResponseModelBase(res);
    }
}