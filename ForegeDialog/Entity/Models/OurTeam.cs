using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;
using Entity.Models.File;

namespace Entity.Models;
[Table("our_teams")]
public class OurTeam : ModelBase<long>
{
    [Column("name", TypeName = "jsonb")] public MultiLanguageField Name { get; set; }
    [Column("role", TypeName = "jsonb")] public MultiLanguageField Role { get; set; }
    [Column("about", TypeName = "jsonb")] public MultiLanguageField About { get; set; }
    [Column("experience", TypeName = "jsonb")] public MultiLanguageField Experience { get; set; }
    [Column("skills", TypeName = "jsonb")] public List<MultiLanguageField> Skills { get; set; }
    [Column("pictures_id"), ForeignKey(nameof(Pictures))]
    public Guid PicturesId { get; set; }
    public virtual FileModel Pictures { get; set; }

}