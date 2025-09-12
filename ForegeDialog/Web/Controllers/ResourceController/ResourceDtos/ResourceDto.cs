using Entity.Models.Common;

namespace Web.Controllers.ResourceController.ResourceDtos;

public class ResourceDto
{
   public long Id { get; set; }
   public MultiLanguageField FileName { get; set; }
   public string FileType { get; set; }
   public MultiLanguageField Subject { get; set; }
   public long ResourceCategoryId { get; set; }
   public DateTime PublishedDate { get; set; }
   public MultiLanguageField Size { get; set; }
   public Guid FileIdUZ { get; set; }
   public Guid FileIdRU { get; set; }
   public Guid FileIdEN { get; set; }
   public Guid FileIdGER { get; set; }
}