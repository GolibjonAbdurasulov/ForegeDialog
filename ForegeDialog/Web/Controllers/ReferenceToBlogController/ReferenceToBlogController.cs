using DatabaseBroker.Repositories.BlogModelRepository;
using DatabaseBroker.Repositories.NewsCategoryRepository;
using DatabaseBroker.Repositories.OurCategoriesRepository;
using DatabaseBroker.Repositories.ReferenceToBlogRepository;
using DatabaseBroker.Repositories.TagsRepository;
using DatabaseBroker.Repositories.ViewsRepository;
using Entity.Exceptions;
using Entity.Models;
using Entity.Models.Blog;
using Entity.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Common;
using Web.Controllers.BlogController.BlogControllerDtos;
using Web.Controllers.ReferenceToBlogController.Dtos;

namespace Web.Controllers.ReferenceToBlogController;


[ApiController]
[Route("[controller]/[action]")]
public class ReferenceToBlogController(
    IOurCategoriesRepository ourCategoriesRepository,
    IReferenceToBlogRepository referenceModelRepository,IBlogModelRepository blogModelRepository,
    ITagsRepository tagsRepository,INewsCategoryRespository newsCategoryRepository, 
    IViewsRepository viewsRepository)
    : ControllerBase
{
    
    private IViewsRepository  ViewsRepository { get; set; } = viewsRepository;

    private ITagsRepository TagsRepository { get; set; } = tagsRepository;
    private INewsCategoryRespository  NewsCategoryRepository { get; set; }=newsCategoryRepository;

    private IBlogModelRepository BlogModelRepository { get; set; } = blogModelRepository;
    private IOurCategoriesRepository OurCategoriesRepository { get; set; } = ourCategoriesRepository;
    private IReferenceToBlogRepository ReferenceToBlogRepository { get; set; } = referenceModelRepository;

    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync( ReferenceToBlogCreationDto dto)
    {
        var entity = new ReferenceToBlog()
        {
            CategoryId = dto.CategoryId,
            Categories = null,
            BlogId = dto.BlogId,
        };

        try
        {
            entity.Categories = await OurCategoriesRepository.GetByIdAsync(dto.CategoryId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Our categories couldn't be found");
            throw;
        }
        var resEntity=await ReferenceToBlogRepository.AddAsync(entity);

        var resDto = new ReferenceToBlogDto
        {
            Id = resEntity.Id,
            CategoryId = resEntity.CategoryId,
            BlogId = resEntity.BlogId,
        };
        return new ResponseModelBase(resDto);
    }


  
    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateAsync( ReferenceToBlogDto dto)
    {
        var res =  await ReferenceToBlogRepository.GetByIdAsync(dto.Id);

        res.CategoryId = dto.CategoryId;
        res.BlogId = dto.BlogId;
      
        await ReferenceToBlogRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }
    
    
    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {
        
        var res =  await ReferenceToBlogRepository.GetByIdAsync(id);
        await ReferenceToBlogRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res =  await ReferenceToBlogRepository.GetByIdAsync(id);

        List<ReferenceToBlogGetDto> blogs=new List<ReferenceToBlogGetDto>();
        
        
        var dto=new ReferenceToBlogGetDto
        {
            Id = res.Id,
            CategoryId = res.CategoryId,
            BlogId = res.BlogId,
            Blog = null
        };
        return new ResponseModelBase(dto);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetReferencesByCategoryIdsAsync(long id)
    {
        var listEntities =   ReferenceToBlogRepository.GetAllAsQueryable().
            Where(item=>item.CategoryId==id).ToList();
        
        if (listEntities == null || listEntities.Count == 0)
            throw new NotFoundException("ReferenceModel not found");
        
        
        List<ReferenceToBlogGetDto> resDtos = new List<ReferenceToBlogGetDto>();
        foreach (ReferenceToBlog model in listEntities)
        {
            
            var blogModel = BlogModelRepository.GetAllAsQueryable().
                FirstOrDefault(item=>item.Id==model.BlogId);
            if (blogModel == null)
            {
                throw new NullReferenceException("ReferenceModel not found on ReferenceToBlog controller");
            }
            var tags = await GetTagsAsync(blogModel.Tags);
            var categories=await GetCategoriesAsync(blogModel.Categories);
            var  viewsCount=ViewsRepository.GetAllAsQueryable().
                ToList().FirstOrDefault(item=>item.ItemId==blogModel.Id)!.Count;

            resDtos.Add(new ReferenceToBlogGetDto
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                BlogId = model.BlogId,
                Blog = new BlogDto
                {
                    Id = blogModel.Id,
                    Subject = blogModel.Subject,
                    Title = blogModel.Title,
                    Text = "text",
                    TagsIds = blogModel.Categories,
                    CategoriesIds = blogModel.Tags,
                    Tags = tags,
                    Categories = categories,
                    Images=blogModel.Images,
                    ReadingTime = blogModel.ReadingTime,
                    PublishedDate = blogModel.PublishedDate,
                    PublisherId = blogModel.PublisherId,
                    ViewsCount = viewsCount
                }
            });
        }
        return new ResponseModelBase(resDtos);
    }
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =   ReferenceToBlogRepository.GetAllAsQueryable().ToList();

        List<ReferenceToBlogDto> resDto = new List<ReferenceToBlogDto>();
        foreach (ReferenceToBlog model in res)
        {
            resDto.Add(new ReferenceToBlogDto()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                BlogId = model.BlogId,
            });
        }
        return new ResponseModelBase(resDto);
    }
    
    private async Task<List<MultiLanguageField>> GetTagsAsync(List<long> tagsIds)
    {
        if (tagsIds == null || tagsIds.Count == 0)
            throw new ArgumentException("tagsIds bo'sh bo'lishi mumkin emas.", nameof(tagsIds));

        return await TagsRepository.GetAllAsQueryable()
            .Where(tag => tagsIds.Contains(tag.Id))
            .Select(tag => tag.TagName)
            .ToListAsync();
    }

    private async Task<List<MultiLanguageField>> GetCategoriesAsync(List<long> categoryIds)
    {
        if (categoryIds == null || categoryIds.Count == 0)
            throw new ArgumentException("categoryIds bo'sh bo'lishi mumkin emas.", nameof(categoryIds));

        return await NewsCategoryRepository.GetAllAsQueryable()
            .Where(category => categoryIds.Contains(category.Id))
            .Select(category => category.CategoryName)
            .ToListAsync();
    }
}