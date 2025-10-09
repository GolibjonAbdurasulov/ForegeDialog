using DatabaseBroker.Repositories.BlogModelRepository;
using DatabaseBroker.Repositories.NewsCategoryRepository;
using DatabaseBroker.Repositories.TagsRepository;
using DatabaseBroker.Repositories.ViewsRepository;
using Entity.Models;
using Entity.Models.Blog;
using Entity.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Common;
using Web.Controllers.BlogController.BlogControllerDtos;

namespace Web.Controllers.BlogController;
[ApiController]
[Route("[controller]/[action]")]
public class BlogController(IBlogModelRepository blogModelRepository,IViewsRepository viewsRepository, 
    ITagsRepository TagsRepository,INewsCategoryRespository newsCategoryRepository) : ControllerBase
{
    private IViewsRepository  ViewsRepository { get; set; } = viewsRepository;
    private IBlogModelRepository BlogModelRepository { get; set; } = blogModelRepository;
    private ITagsRepository TagsRepository { get; set; } = TagsRepository;
    private INewsCategoryRespository  NewsCategoryRepository { get; set; }=newsCategoryRepository;


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
        
        var tags = await GetTagsAsync(resEntity.Tags);
        var categories=await GetCategoriesAsync(resEntity.Categories);

        await ViewsRepository.AddAsync(new Views
        {
            ItemId = resEntity.Id,
            Count = 0
        });
        
        var resDto = new BlogDto
        {
            Id = resEntity.Id,
            Subject = resEntity.Subject,
            Title = resEntity.Title,
            Text = resEntity.Text,
            Categories = categories,
            Tags = tags,            
            CategoriesIds = resEntity.Categories,
            TagsIds = resEntity.Tags,
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
        res.Categories = dto.CategoriesIds;
        res.Tags = dto.TagsIds;
        res.ReadingTime = dto.ReadingTime;
        res.Images = dto.Images;
        res.PublishedDate = dto.PublishedDate;
        res.PublisherId = dto.PublisherId;

      
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
        
        var viewsCounter= ViewsRepository.GetAllAsQueryable().FirstOrDefault(item => item.ItemId == id);

        int n=0;
        if (viewsCounter is not null)
        {
            ++viewsCounter.Count;
            await ViewsRepository.UpdateAsync(viewsCounter);
            n = viewsCounter.Count;
        }
        var tags = await GetTagsAsync(res.Tags);
        var categories=await GetCategoriesAsync(res.Categories);

        var dto = new BlogDto
        {
            Id = res.Id,
            Subject = res.Subject,
            Title = res.Title,
            Text = res.Text,
            Categories = categories,
            Tags = tags,
            TagsIds = res.Tags,
            CategoriesIds = res.Categories,
            Images = res.Images,
            ReadingTime = res.ReadingTime,
            PublishedDate = res.PublishedDate,
            PublisherId = res.PublisherId,
            ViewsCount = n
        };
        return new ResponseModelBase(dto);
    }
    [HttpGet]
    
    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        // 1️⃣ Butun bloglarni olish, trackingni o'chirish
        var blogs = await BlogModelRepository.GetAllAsQueryable()
            .AsNoTracking()
            .ToListAsync();

        // 2️⃣ Barcha tags va categories ni oldindan olish
        var allTags = await TagsRepository.GetAllAsQueryable().AsNoTracking().ToListAsync();
        var allCategories = await NewsCategoryRepository.GetAllAsQueryable().AsNoTracking().ToListAsync();

        // 3️⃣ Barcha views ni oldindan olish
        var allViews = await ViewsRepository.GetAllAsQueryable()
            .AsNoTracking()
            .GroupBy(v => v.ItemId)
            .Select(g => new { ItemId = g.Key, Count = g.Sum(v => v.Count) })
            .ToListAsync();

        // 4️⃣ DTO yaratish
        var dtos = blogs.Select(blog =>
        {
            var tags = allTags.Where(t => blog.Tags.Contains(t.Id)).Select(t => t.TagName).ToList();
            var categories = allCategories.Where(c => blog.Categories.Contains(c.Id))
                .Select(c => c.CategoryName)
                .ToList();
            var viewsCount = allViews.FirstOrDefault(v => v.ItemId == blog.Id)?.Count ?? 0;

            return new BlogDto
            {
                Id = blog.Id,
                Subject = blog.Subject,
                Title = blog.Title,
                Text = blog.Text,
                TagsIds = blog.Tags,
                CategoriesIds = blog.Categories,
                Tags = tags,
                Categories = categories,
                Images = blog.Images,
                ReadingTime = blog.ReadingTime,
                PublishedDate = blog.PublishedDate,
                PublisherId = blog.PublisherId,
                ViewsCount = viewsCount
            };
        }).ToList();

        return new ResponseModelBase(dtos);
    }

    
    // [HttpGet]
    // public async Task<ResponseModelBase> GetAllAsync()
    // {
    //     var resNews =   BlogModelRepository.GetAllAsQueryable().ToList();
    //     List<BlogDto> dtos = new List<BlogDto>();
    //
    //     foreach (BlogModel res in resNews)
    //     {
    //         var tags = await GetTagsAsync(res.Tags);
    //         var categories=await GetCategoriesAsync(res.Categories);
    //
    //         var  n=ViewsRepository.GetAllAsQueryable().ToList().FirstOrDefault(item=>item.ItemId==res.Id)!.Count;
    //         
    //         dtos.Add(new BlogDto()
    //         {
    //             Id = res.Id,
    //             Subject = res.Subject,
    //             Title = res.Title,
    //             Text = res.Text,
    //             TagsIds = res.Categories,
    //             CategoriesIds = res.Tags,
    //             Tags = tags,
    //             Categories = categories,
    //             Images=res.Images,
    //             ReadingTime = res.ReadingTime,
    //             PublishedDate = res.PublishedDate,
    //             PublisherId = res.PublisherId,
    //             ViewsCount = n
    //
    //         });
    //     }
    //     
    //     return new ResponseModelBase(dtos);
    // }
    //
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
    
    // [HttpGet]
    // public async Task<ResponseModelBase> GetAllByNewestAsync()
    // {
    //     var resBlog = BlogModelRepository.GetAllAsQueryable()
    //         .OrderByDescending(n => n.PublishedDate)  // yangi birinchi
    //         .ToList();
    //
    //     List<BlogDto> dtos = new List<BlogDto>();
    //
    //     foreach (BlogModel res in resBlog)
    //     {
    //         var tags = await GetTagsAsync(res.Tags);
    //         var categories = await GetCategoriesAsync(res.Categories);
    //         var  n=ViewsRepository.GetAllAsQueryable().ToList().FirstOrDefault(item=>item.ItemId==res.Id).Count;
    //
    //         dtos.Add(new BlogDto()
    //         {
    //             Id = res.Id,
    //             Subject = res.Subject,
    //             Title = res.Title,
    //             Text = res.Text,
    //             TagsIds = res.Tags,
    //             CategoriesIds = res.Categories,
    //             Tags = tags,
    //             Categories = categories,
    //             Images = res.Images,
    //             ReadingTime = res.ReadingTime,
    //             PublishedDate = res.PublishedDate,
    //             PublisherId = res.PublisherId,
    //             ViewsCount = n
    //         });
    //     }
    //
    //     return new ResponseModelBase(dtos);
    // }
    //
    // [HttpGet]
    // public async Task<ResponseModelBase> GetAllByOldestAsync()
    // {
    //     var resBlog = BlogModelRepository.GetAllAsQueryable()
    //         .OrderBy(n => n.PublishedDate)  // eski birinchi
    //         .ToList();
    //
    //     List<BlogDto> dtos = new List<BlogDto>();
    //
    //     foreach (BlogModel res in resBlog)
    //     {
    //         var tags = await GetTagsAsync(res.Tags);
    //         var categories = await GetCategoriesAsync(res.Categories);
    //         var  n=ViewsRepository.GetAllAsQueryable().ToList().FirstOrDefault(item=>item.ItemId==res.Id).Count;
    //         dtos.Add(new BlogDto()
    //         {
    //             Id = res.Id,
    //             Subject = res.Subject,
    //             Title = res.Title,
    //             Text = res.Text,
    //             TagsIds = res.Tags,
    //             CategoriesIds = res.Categories,
    //             Tags = tags,
    //             Categories = categories,
    //             Images = res.Images,
    //             ReadingTime = res.ReadingTime,
    //             PublishedDate = res.PublishedDate,
    //             PublisherId = res.PublisherId,
    //             ViewsCount = n
    //         });
    //     }
    //
    //     return new ResponseModelBase(dtos);
    // }

    [HttpGet]
public async Task<ResponseModelBase> GetAllByNewestAsync()
{
    return await GetAllOrderedAsync(orderByDescending: true);
}

[HttpGet]
public async Task<ResponseModelBase> GetAllByOldestAsync()
{
    return await GetAllOrderedAsync(orderByDescending: false);
}

private async Task<ResponseModelBase> GetAllOrderedAsync(bool orderByDescending)
{
    var blogsQuery = BlogModelRepository.GetAllAsQueryable().AsNoTracking();
    blogsQuery = orderByDescending 
        ? blogsQuery.OrderByDescending(b => b.PublishedDate)
        : blogsQuery.OrderBy(b => b.PublishedDate);

    var blogs = await blogsQuery.ToListAsync();

    var allTags = await TagsRepository.GetAllAsQueryable().AsNoTracking().ToListAsync();
    var allCategories = await NewsCategoryRepository.GetAllAsQueryable().AsNoTracking().ToListAsync();
    var allViews = await ViewsRepository.GetAllAsQueryable()
        .AsNoTracking()
        .GroupBy(v => v.ItemId)
        .Select(g => new { ItemId = g.Key, Count = g.Sum(v => v.Count) })
        .ToListAsync();

    var dtos = blogs.Select(blog =>
    {
        var tags = allTags.Where(t => blog.Tags.Contains(t.Id)).Select(t => t.TagName).ToList();
        var categories = allCategories.Where(c => blog.Categories.Contains(c.Id))
                                     .Select(c => c.CategoryName)
                                     .ToList();
        var viewsCount = allViews.FirstOrDefault(v => v.ItemId == blog.Id)?.Count ?? 0;

        return new BlogDto
        {
            Id = blog.Id,
            Subject = blog.Subject,
            Title = blog.Title,
            Text = blog.Text,
            TagsIds = blog.Tags,
            CategoriesIds = blog.Categories,
            Tags = tags,
            Categories = categories,
            Images = blog.Images,
            ReadingTime = blog.ReadingTime,
            PublishedDate = blog.PublishedDate,
            PublisherId = blog.PublisherId,
            ViewsCount = viewsCount
        };
    }).ToList();

    return new ResponseModelBase(dtos);
}

    
   /* [HttpPost]
    public async Task<ResponseModelBase> GetByTagsAsync([FromBody]List<long> tagsIds)
    {
        var res = await GetBlogWithTags(tagsIds);
        return new ResponseModelBase(res);
    }
    
    [HttpPost]
    public async Task<ResponseModelBase> GetByCategoriesAsync([FromBody]List<long> categoryIds)
    {
        var res = await GetBlogWithCategory(categoryIds);
        return new ResponseModelBase(res);
    }
    
    private async Task<List<BlogDto>> GetBlogWithTags(List<long> tagsIds)
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
    
    
    private async Task<List<BlogDto>> GetBlogWithCategory(List<long> categoryIds)
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
    }*/

    
}