using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models;
[Table("views")]
public class Views: ModelBase<long>
{
    [Column("item_id")]public long ItemId { get; set; }
    [Column("count")]public int Count { get; set; }
}