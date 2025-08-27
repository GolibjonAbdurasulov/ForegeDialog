using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("news_category")]
public class NewsCategory : ModelBase<long>
{
   [Column("category_name",TypeName = "jsonb")] public MultiLanguageField CategoryName { get; set; }   
}