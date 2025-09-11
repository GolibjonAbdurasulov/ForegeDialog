using DatabaseBroker.Repositories.NewsRepository;
using DatabaseBroker.Repositories.ViewsRepository;
using Entity.Models;
using Entity.Models.Blog;
using Entity.Models.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.BlogController.BlogControllerDtos;
using Web.Controllers.NewsController.NewsDtos;

namespace Web.Controllers.NewsController;
[ApiController]
[Route("[controller]/[action]")]
public class NewsController(INewsRepository newsRepository,IViewsRepository viewsRepository) : ControllerBase
{
    private INewsRepository NewsRepository { get; set; } = newsRepository;
    private IViewsRepository  ViewsRepository { get; set; } = viewsRepository;

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( NewsCreationDto dto)
    {
        var entity = new News()
        {
            Subject = dto.Subject,
            Title = dto.Title,
            Text = dto.Text,
            Categories = dto.Categories,
            Tags = dto.Tags,
            Images = dto.Images,
            ReadingTime = dto.ReadingTime,
            PublishedDate = dto.PublishedDate,
            PublisherId = dto.PublisherId
        };
        var resEntity=await NewsRepository.AddAsync(entity);

        await ViewsRepository.AddAsync(new Views
        {
            ItemId = resEntity.Id,
            Count = 0
        });
        
        var resDto = new NewsDto()
        {
            Id = resEntity.Id,
            Subject = resEntity.Subject,
            Title = resEntity.Title,
            Text = resEntity.Text,
            Categories = resEntity.Categories,
            Tags = resEntity.Tags,
            Images = resEntity.Images,
            ReadingTime = resEntity.ReadingTime,
            PublishedDate = resEntity.PublishedDate,
            PublisherId = resEntity.PublisherId,
            ViewsCount = 0
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( NewsDto dto)
    {
        var res =  await NewsRepository.GetByIdAsync(dto.Id);
        res.Subject = dto.Subject;
        res.Title = dto.Title;
        res.Text = dto.Text;
        res.Categories = dto.Categories;
        res.ReadingTime = dto.ReadingTime;
        res.PublishedDate = dto.PublishedDate;
        res.PublisherId = dto.PublisherId;
        res.Tags = dto.Tags;
        res.Images = dto.Images;
      
        await NewsRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await NewsRepository.GetByIdAsync(id);
        await NewsRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await NewsRepository.GetByIdAsync(id);
        
        var viewsCounter= ViewsRepository.GetAllAsQueryable().FirstOrDefault(item => item.ItemId == id);
        int n = 0;
        if (viewsCounter is not null)
        {
            ++viewsCounter.Count;
            await ViewsRepository.UpdateAsync(viewsCounter);
            n=viewsCounter.Count;
        }
        var dto = new NewsDto
        {
            Id = res.Id,
            Subject = res.Subject,
            Title = res.Title,
            Text = res.Text,
            Categories = res.Categories,
            Tags = res.Tags,
            Images = res.Images,
            ReadingTime = res.ReadingTime,
            PublishedDate = res.PublishedDate,
            PublisherId = res.PublisherId,
            ViewsCount = n
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var resNews =   NewsRepository.GetAllAsQueryable().ToList();
        List<NewsDto> dtos = new List<NewsDto>();
        int n = 0;
        foreach (News res in resNews)
        {
            var counter= ViewsRepository.GetAllAsQueryable().FirstOrDefault(item => item.ItemId == res.Id);
         
            if  (counter is not null) n=counter.Count;
            
            dtos.Add(new NewsDto()
            {
                Id = res.Id,
                Subject = res.Subject,
                Title = res.Title,
                Text = res.Text,
                Categories = res.Categories,
                Tags = res.Tags,
                Images=res.Images,
                ReadingTime = res.ReadingTime,
                PublishedDate = res.PublishedDate,
                PublisherId = res.PublisherId,
                ViewsCount = n
            });
        }
        
        return new ResponseModelBase(dtos);
    }

    [HttpPost]
    public async Task<ResponseModelBase> GetByTagsAsync([FromBody] List<long> tagsIds)
    {
        var res = await GetNewsWithTags(tagsIds);
        return new ResponseModelBase(res);
    }

    
    [HttpPost]
    public async Task<ResponseModelBase> GetByCategoriesAsync([FromBody]List<long> categoryIds)
    {
        var res = await GetNewsWithCategory(categoryIds);
        return new ResponseModelBase(res);
    }
    
    private async Task<List<NewsDto>> GetNewsWithTags(List<long> tagsIds)
    {
        var allNews = NewsRepository.GetAllAsQueryable().ToList();
        
        var models = allNews
            .Where(news => news.Tags != null && news.Tags.Any(tagsIds.Contains))
            .ToList();

        var dtos = new List<NewsDto>();
        foreach (News news in models)
        {
            dtos.Add(new NewsDto
            {
                Id = news.Id,
                Subject = news.Subject,
                Title = news.Title,
                Text = news.Text,
                Categories = news.Categories,
                Tags = news.Tags,
                Images = news.Images,
                ReadingTime = news.ReadingTime,
                PublishedDate = news.PublishedDate,
                PublisherId = news.PublisherId
            });
        }
        return dtos;
    }
    
    
    private async Task<List<NewsDto>> GetNewsWithCategory(List<long> categoryIds)
    {
        var allNews = NewsRepository.GetAllAsQueryable().ToList();
        
        var models = allNews
            .Where(news => news.Tags != null && news.Categories.Any(categoryIds.Contains))
            .ToList();

        var dtos = new List<NewsDto>();
        foreach (News news in models)
        {
            dtos.Add(new NewsDto
            {
                Id = news.Id,
                Subject = news.Subject,
                Title = news.Title,
                Text = news.Text,
                Categories = news.Categories,
                Tags = news.Tags,
                ReadingTime = news.ReadingTime,
                PublishedDate = news.PublishedDate,
                PublisherId = news.PublisherId
            });
        }
        return dtos;
    }

}