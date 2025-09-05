using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.File;

[Table("file_model")]
public class FileModel : ModelBase<Guid>
{
    [Column("file_name")] public virtual string FileName { get; set; }
    [Column("content_type")] public string ContentType { get; set; }
    [Column("path")] public virtual string Path { get; set; }
}