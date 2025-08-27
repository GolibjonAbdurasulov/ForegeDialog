using Entity.Models.Common;

namespace Web.Controllers.OurCategoriesController.OurCategoriesDtos;

public class OurCategoriesCreationDto
{
    public MultiLanguageField Name { get; set; }
    public Guid PicturesId { get; set; }
}