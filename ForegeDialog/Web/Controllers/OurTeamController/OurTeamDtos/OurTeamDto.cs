using Entity.Models.Common;

namespace Web.Controllers.OurTeamController.OurTeamDtos;

public class OurTeamDto
{
    public long Id { get; set; }
    public MultiLanguageField Name { get; set; }
    public MultiLanguageField Role { get; set; }
    public MultiLanguageField About { get; set; }
    public MultiLanguageField Experience { get; set; }
    public List<MultiLanguageField> Skills { get; set; }
    public Guid PicturesId { get; set; }
}