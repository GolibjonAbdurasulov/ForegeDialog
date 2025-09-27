using Entity.Models.Common;

namespace Web.Controllers.PicturesModelController.Dtos;

public class PicturesModelUpdateDto
{
    public long Id { get; set; }
    public long  CategoryId { get; set; }
    public MultiLanguageField  CategoryName { get; set; }
}