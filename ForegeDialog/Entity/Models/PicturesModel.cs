using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("pictures_models")]
public class PicturesModel : ModelBase<long>
{
    [Column("category_id"),ForeignKey(nameof(ImageCategory))]public long CategoryId { get; set; }
    public virtual ImageCategory ImageCategory { get; set; }
    [Column("images")]public List<long> Images { get; set; }
}