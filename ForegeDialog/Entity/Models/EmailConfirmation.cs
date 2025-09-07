using System;

namespace Entity.Models;

public class EmailConfirmation
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }
    public DateTime ExpireTime { get; set; }
    public bool IsUsed { get; set; }
}
