using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("our_partners")]
public class OurPartners : ModelBase<long>
{
    [Column("name", TypeName = "jsonb")]public MultiLanguageField Name { get; set; }
    [Column("about", TypeName = "jsonb")]public MultiLanguageField About { get; set; }
    [Column("link")]public string Link { get; set; }
    [Column("pictures_id")]public Guid PicturesId  { get; set; }
}