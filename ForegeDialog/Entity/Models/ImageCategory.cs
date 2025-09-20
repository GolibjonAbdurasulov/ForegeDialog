using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("image_category")]
public class ImageCategory : ModelBase<long>
{
    [Column("category",TypeName = "jsonb"),]public  MultiLanguageField Category { get; set; }
}