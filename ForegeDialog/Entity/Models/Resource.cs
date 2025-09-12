#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("resources")]
public class Resources : ModelBase<long>
{
    [Column("file_name", TypeName = "jsonb")] 
    public MultiLanguageField FileName { get; set; }

    [Column("file_type")] 
    public string? FileType { get; set; }

    [Column("subject", TypeName = "jsonb")] 
    public MultiLanguageField Subject { get; set; }

    [Column("resource_category_id")] 
    public long ResourceCategoryId { get; set; }

    [Column("published_date")] 
    public DateTime PublishedDate { get; set; }

    [Column("size", TypeName = "jsonb")] 
    public MultiLanguageField? Size { get; set; }

    [Column("file_id_uz")] 
    public Guid FileIdUZ { get; set; }

    [Column("file_id_ru")] 
    public Guid FileIdRU { get; set; }

    [Column("file_id_en")] 
    public Guid FileIdEN { get; set; }

    [Column("file_id_ger")] 
    public Guid FileIdGER { get; set; }
}