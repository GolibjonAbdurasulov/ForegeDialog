using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entity.Models.News;
[Table("news")]
public class News : ModelBase<long>
{
    [Column("subject",TypeName = "jsonb")] public MultiLanguageField Subject { get; set; }   
    [Column("title",TypeName = "jsonb")] public MultiLanguageField Title { get; set; }   
    [Column("text",TypeName = "jsonb")] public MultiLanguageField Text { get; set; }   
    [Column("categories")] public List<long> Categories { get; set; }   
    [Column("tags")] public List<long> Tags { get; set; }   
    [Column("reading_time")] public string ReadingTime{ get; set; }   
    [Column("images")] public List<Guid> Images{ get; set; }  
    [Column("published_date")] public DateTime PublishedDate { get; set; } 
    [Column("publisher_id"),ForeignKey(nameof(Publisher))] public long PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
}