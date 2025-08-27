using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Web.Controllers.NewsCategoryController.NewsCategoryDto;

public class NewsCategoryCreationDto
{
    [Column("category_name",TypeName = "jsonb")] public MultiLanguageField CategoryName { get; set; }   

}