using Entity.Models.Common;

namespace Web.Controllers.OurCategoriesController.OurCategoriesDtos;

public class OurCategoriesDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public Guid PicturesId { get; set; }
}