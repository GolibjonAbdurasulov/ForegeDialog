using Entity.Models.Common;

namespace Web.Controllers.OurPartnersController.OurPartnersDtos;

public class OurPartnersCreationDto
{
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField About { get; set; }
    public string Link { get; set; }
    public Guid PicturesId  { get; set; }
}