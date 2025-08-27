using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models;
[Table("our_categories")]
public class OurCategories : ModelBase<long>
{
    [Column("name", TypeName = "jsonb")]public MultiLanguageField Name { get; set; }
    [Column("pictures_id"), ForeignKey(nameof(Pictures))]
    public Guid PicturesId { get; set; }
    public virtual FileModel Pictures { get; set; }
    
    
}