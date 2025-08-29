#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models;
[Table("resource")]
public class Resource : ModelBase<long>
{
    [Column("file_name",TypeName = "jsonb")]public MultiLanguageField FileName { get; set; }
    [Column("file_type")]public string? FileType { get; set; }
    [Column("subject",TypeName = "jsonb")]public MultiLanguageField Subject { get; set; }
    [Column("published_date")]public DateTime PublishedDate { get; set; }
    [Column("size")]public string? Size { get; set; }
    [Column("file_id"),ForeignKey(nameof(File))]public Guid FileId { get; set; }
    public virtual FileModel File { get; set; }
}