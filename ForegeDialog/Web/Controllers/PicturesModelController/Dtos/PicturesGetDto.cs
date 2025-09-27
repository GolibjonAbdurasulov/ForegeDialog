using Entity.Models;
using Entity.Models.Common;

namespace Web.Controllers.PicturesModelController.Dtos;

public class PicturesGetDto
{
    public long Id { get; set; }
    public long  CategoryId { get; set; }
    public MultiLanguageField  CategoryName { get; set; }
    public List<long> ImagesIds { get; set; }
    public List<ImageModel>  ImageModels { get; set; }
}