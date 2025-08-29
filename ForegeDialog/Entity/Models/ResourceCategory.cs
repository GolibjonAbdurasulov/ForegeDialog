using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("resourceCategory")]
public class ResourceCategory : ModelBase<long>
{
    [Column("category_name",TypeName = "jsonb")] public MultiLanguageField CategoryName { get; set; }   

}