using DatabaseBroker.Repositories.BlogModelRepository;
using Entity.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.BlogController.BlogControllerDtos;

namespace Web.Controllers.BlogController;
[ApiController]
[Route("[controller]/[action]")]
public class BlogController(IBlogModelRepository blogModelRepository) : ControllerBase
{
    private IBlogModelRepository BlogModelRepository { get; set; } = blogModelRepository;

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( BlogCreationDto dto)
    {
        var entity = new BlogModel
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
        var resEntity=await BlogModelRepository.AddAsync(entity);
        
        var resDto = new BlogDto
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
            PublisherId = resEntity.PublisherId
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( BlogDto dto)
    {
        var res =  await BlogModelRepository.GetByIdAsync(dto.Id);
        res.Subject = dto.Subject;
        res.Title = dto.Title;
        res.Text = dto.Text;
        res.Categories = dto.Categories;
        res.ReadingTime = dto.ReadingTime;
        res.Images = dto.Images;
        res.PublishedDate = dto.PublishedDate;
        res.PublisherId = dto.PublisherId;
        res.Tags = dto.Tags;
      
        await BlogModelRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await BlogModelRepository.GetByIdAsync(id);
        await BlogModelRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await BlogModelRepository.GetByIdAsync(id);
        var dto = new BlogDto
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
            PublisherId = res.PublisherId
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var resBlog =   BlogModelRepository.GetAllAsQueryable().ToList();
        List<BlogDto> dtos = new List<BlogDto>();
        foreach (BlogModel res in resBlog)
        {
            dtos.Add(new BlogDto()
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
                PublisherId = res.PublisherId
            });
        }
        
        return new ResponseModelBase(dtos);
    }   
    
    [HttpPost]
    public async Task<ResponseModelBase> GetByTagsAsync([FromBody]List<long> tagsIds)
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
    
    private async Task<List<BlogDto>> GetNewsWithTags(List<long> tagsIds)
    {
        var allBlogs = BlogModelRepository.GetAllAsQueryable().ToList();
        
        var models = allBlogs
            .Where(blog => blog.Tags != null && blog.Tags.Any(tagsIds.Contains))
            .ToList();

        var dtos = new List<BlogDto>();
        foreach (BlogModel blog in models)
        {
            dtos.Add(new BlogDto
            {
                Id = blog.Id,
                Subject = blog.Subject,
                Title = blog.Title,
                Text = blog.Text,
                Categories = blog.Categories,
                Tags = blog.Tags,
                Images = blog.Images,
                ReadingTime = blog.ReadingTime,
                PublishedDate = blog.PublishedDate,
                PublisherId = blog.PublisherId
            });
        }
        return dtos;
    }
    
    
    private async Task<List<BlogDto>> GetNewsWithCategory(List<long> categoryIds)
    {
        var allBlog = BlogModelRepository.GetAllAsQueryable().ToList();
        
        var models = allBlog
            .Where(blog => blog.Tags != null && blog.Categories.Any(categoryIds.Contains))
            .ToList();

        var dtos = new List<BlogDto>();
        foreach (BlogModel blog in models)
        {
            dtos.Add(new BlogDto
            {
                Id = blog.Id,
                Subject = blog.Subject,
                Title = blog.Title,
                Text = blog.Text,
                Categories = blog.Categories,
                Tags = blog.Tags,
                Images = blog.Images,
                ReadingTime = blog.ReadingTime,
                PublishedDate = blog.PublishedDate,
                PublisherId = blog.PublisherId
            });
        }
        return dtos;
    }

    
}