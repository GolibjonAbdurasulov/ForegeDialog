using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;

public class ImageModel : ModelBase<long>
{
    [Column("file_id")]public long FileId { get; set; }
    [Column("image_name")]public string ImageName { get; set; }
}