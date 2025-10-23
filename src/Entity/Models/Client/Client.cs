using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Client;

[Table("clients")]
public class Client : ModelBase<long>
{
    [Column("full_name")] public string FullName { get; set; }   
    [Column("email")] public string Email { get; set; }   
    [Column("password")] public string Password { get; set; }   
    [Column("IsSigned")] public bool IsSigned { get; set; }
     
}