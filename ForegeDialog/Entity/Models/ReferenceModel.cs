using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;

[Table("reference_models")]
public class ReferenceModel : ModelBase<long>
{
    [Column("category_id"),ForeignKey(nameof(Categories))]public long  CategoryId { get; set; }
    public virtual OurCategories Categories { get; set; }
    
    [Column("pictures_model_id")]public long  PicturesModelId { get; set; }
}