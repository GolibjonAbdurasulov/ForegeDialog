using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("tags")]
public class Tags : ModelBase<long>
{
    [Column("category_name",TypeName = "jsonb")] public MultiLanguageField TagName { get; set; }   
}