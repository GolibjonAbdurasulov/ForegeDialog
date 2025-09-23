using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("reference_to_blog")]
public class ReferenceToBlog : ModelBase<long>
{
    [Column("category_id"),ForeignKey(nameof(Categories))]public long  CategoryId { get; set; }
    public virtual OurCategories Categories { get; set; }
    
    [Column("blog_id")]public long  BlogId { get; set; }
}