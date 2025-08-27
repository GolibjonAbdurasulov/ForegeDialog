using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;

[Table("statistics")]
public class Statistics : ModelBase<long>
{
    [Column("happy_clients", TypeName = "jsonb")]public int HappyClients { get; set; }
    [Column("projects", TypeName = "jsonb")]public int Projects { get; set; }
    [Column("team_members", TypeName = "jsonb")]public int TeamMembers { get; set; }
    [Column("years_experience", TypeName = "jsonb")]public int YearsExperience { get; set; }
}