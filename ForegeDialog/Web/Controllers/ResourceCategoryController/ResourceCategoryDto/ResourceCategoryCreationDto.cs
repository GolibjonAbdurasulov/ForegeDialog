using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Web.Controllers.ResourceCategoryController.ResourceCategoryDto;

public class ResourceCategoryCreationDto
{
    [Column("category_name",TypeName = "jsonb")] public MultiLanguageField CategoryName { get; set; }   

}