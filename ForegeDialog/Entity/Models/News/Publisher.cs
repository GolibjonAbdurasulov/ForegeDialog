using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.News;
[Table("publishers")]
public class Publisher : ModelBase<long>
{
    [Column("name")]public string Name { get; set; }
    [Column("image_id")]public Guid ImageId { get; set; }
}