using DatabaseBroker.PicturesModelRepository;
using DatabaseBroker.Repositories.ImageCategoryRepository;
using DatabaseBroker.Repositories.ImageModelRepository;
using DatabaseBroker.Repositories.ResourceCategoryRepository;
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
    public async Task<ResponseModelBase> UpdateModelAsync(PicturesDto dto)
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
    public async Task<ResponseModelBase> UpdateImagesAsync(PicturesDto dto)
    {
        var res = await PicturesModelRepository.GetByIdAsync(dto.Id);

        if (res.Images.Equals(dto.ImagesIds))
        {
            res.Images = dto.ImagesIds;
            await PicturesModelRepository.UpdateAsync(res);
            return new ResponseModelBase(dto);
        }

        throw new Exception("Images Update");
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

        var dto = new PicturesDto
        {
            Id = res.Id,
            CategoryId = res.CategoryId,
            CategoryName = res.ImageCategory.Category,
            ImagesIds = res.Images
        };


        return new ResponseModelBase(dto);
    }

    [HttpGet]
    public async Task<ResponseModelBase> GetAllAsync()
    {
        var res =PicturesModelRepository.GetAllAsQueryable().ToList();

        var dtoList = res.Select(item => new PicturesDto
        {
            Id = item.Id,
            CategoryId = item.CategoryId,
            CategoryName = item.ImageCategory.Category,
            ImagesIds = item.Images
        }).ToList();

        return new ResponseModelBase(dtoList);
    }
}