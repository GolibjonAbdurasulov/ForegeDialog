using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("image_model")]
public class ImageModel : ModelBase<long>
{
    [Column("file_id")]public Guid FileId { get; set; }
    [Column("image_name")]public string ImageName { get; set; }
}