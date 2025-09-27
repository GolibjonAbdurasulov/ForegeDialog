using DatabaseBroker.Repositories.ImageCategoryRepository;
using DatabaseBroker.Repositories.ImageModelRepository;
using DatabaseBroker.Repositories.PicturesModelRepository;
using DatabaseBroker.Repositories.ResourceCategoryRepository;
using Entity.Exceptions;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;
using Web.Controllers.PicturesModelController.Dtos;
using Web.Controllers.ResourceCategoryController.ResourceCategoryDto;

namespace Web.Controllers.PicturesModelController;

[ApiController]
[Route("[controller]/[action]")]
public class PicturesModelController(
    IPicturesModelRepository picturesModelRepository,
    IImageCategoryRepository imageCategoryRepository,
    IImageModelRepository imageModelRepository)
    : ControllerBase
{
    private IPicturesModelRepository PicturesModelRepository { get; set; } = picturesModelRepository;
    private IImageCategoryRepository ImageCategoryRepository { get; set; } = imageCategoryRepository;
    private IImageModelRepository ImageModelRepository { get; set; } = imageModelRepository;


    [HttpPost]
    [Authorize]
    public async Task<ResponseModelBase> CreateAsync(PicturesCreationDto dto)
    {

        var entity = new PicturesModel
        {
            CategoryId = dto.CategoryId,
            ImageCategory = null,
            Images = dto.ImagesIds
        };
        var imageCategory = await ImageCategoryRepository.GetByIdAsync(dto.CategoryId);

        if (imageCategory == null)
            entity.ImageCategory = imageCategory;

        var resEntity = await PicturesModelRepository.AddAsync(entity);

        var resDto = new PicturesDto
        {
            Id = resEntity.Id,
            CategoryId = resEntity.CategoryId,
            CategoryName = resEntity.ImageCategory.Category,
            ImagesIds = resEntity.Images
        };

        return new ResponseModelBase(resDto);
    }



    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateModelAsync(PicturesModelUpdateDto dto)
    {
        var res = await PicturesModelRepository.GetByIdAsync(dto.Id);

        res.CategoryId = dto.CategoryId;

        var imageCategory = await ImageCategoryRepository.GetByIdAsync(dto.CategoryId);
        if (imageCategory.Category.Equals(dto.CategoryName))
        {
            imageCategory.Category = dto.CategoryName;
            await ImageCategoryRepository.UpdateAsync(imageCategory);
        }

        await PicturesModelRepository.UpdateAsync(res);
        return new ResponseModelBase(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<ResponseModelBase> UpdateImagesAsync(long picturesModelId,List<long> imagesIds)
    {
        var res = await PicturesModelRepository.GetByIdAsync(picturesModelId);
        
            res.Images = imagesIds;
            await PicturesModelRepository.UpdateAsync(res);
            return new ResponseModelBase(imagesIds);
    }


    [HttpDelete]
    [Authorize]
    public async Task<ResponseModelBase> DeleteAsync(long id)
    {

        var res = await PicturesModelRepository.GetByIdAsync(id);
        await PicturesModelRepository.RemoveAsync(res);
        return new ResponseModelBase(res);
    }

    [HttpGet]
    public async Task<ResponseModelBase> GetByIdAsync(long id)
    {
        var res = await PicturesModelRepository.GetByIdAsync(id);

        if (res is null)
            throw new NotFoundException("PicturesModel Not Found on PicturesModelController");

        var images = ImageModelRepository
            .GetAllAsQueryable()
            .Where(item => res.Images.Contains(item.Id))
            .ToList();

        if (images is null || images.Count == 0)
            throw new NotFoundException( "\n images Not Found on get all");

        var dto = new PicturesGetDto
        {
            Id = res.Id,
            CategoryId = res.CategoryId,
            CategoryName = res.ImageCategory.Category,
            ImagesIds = res.Images,
            ImageModels = images
        };


        return new ResponseModelBase(dto);
    }

    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res = PicturesModelRepository.GetAllAsQueryable().ToList();

        List<PicturesGetDto> dtos = new List<PicturesGetDto>();
        foreach (PicturesModel model in res)
        {
            try
            {
                var images = ImageModelRepository
                    .GetAllAsQueryable()
                    .Where(item => model.Images.Contains(item.Id))
                    .ToList();

                var dto = new PicturesGetDto
                {
                    Id = model.Id,
                    CategoryId = model.CategoryId,
                    CategoryName = model.ImageCategory.Category,
                    ImagesIds = model.Images,
                    ImageModels = images
                };
                dtos.Add(dto);
            }
            catch (Exception e)
            {
                throw new NotFoundException(e + "\n images Not Found on get all");
            }
            
        }
        return new ResponseModelBase(dtos); 
    }
}
