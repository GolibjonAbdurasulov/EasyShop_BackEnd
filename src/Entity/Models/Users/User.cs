using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enums;
using Entity.Models.Common;

namespace Entity.Models.Users;
[Table("users")]
public class User : ModelBase<long>
{
    [Column("full_name")] public virtual string FullName { get; set; }
    [Column("email")] public virtual string Email { get; set; }
    [Column("password")] public virtual string Password { get; set; }
    [Column("role")] public virtual Role Role { get; set; }
    [Column("IsSigned")] public virtual bool IsSigned { get; set; }
}