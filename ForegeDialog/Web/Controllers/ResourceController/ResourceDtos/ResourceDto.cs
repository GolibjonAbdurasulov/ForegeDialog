using Entity.Models.Common;

namespace Web.Controllers.ResourceController.ResourceDtos;

public class ResourceDto
{
   public long Id { get; set; }
   public MultiLanguageField FileName { get; set; }
   public string FileType { get; set; }
   public MultiLanguageField Subject { get; set; }
   public DateTime PublishedDate { get; set; }
   public string Size { get; set; }
   public Guid FileId { get; set; }
}