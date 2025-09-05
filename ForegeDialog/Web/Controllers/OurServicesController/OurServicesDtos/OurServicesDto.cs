using Entity.Models.Common;

namespace Web.Controllers.OurServicesController.OurServicesDtos;

public class OurServicesDto
{
    public long Id { get; set; }
    public MultiLanguageField Title { get; set; }
    public MultiLanguageField Description { get; set; }
    public Guid PicturesId { get; set; }

}