using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;

public class ImageCategory : ModelBase<long>
{
    [Column("category",TypeName = "jsonb"),]public  MultiLanguageField Category { get; set; }
}