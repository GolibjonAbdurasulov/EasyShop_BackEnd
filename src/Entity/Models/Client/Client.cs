using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Client;

[Table("clients")]
public class Client : ModelBase<long>
{
    [Column("client_full_name")] public string ClientFullName { get; set; }   
    [Column("company_name")] public string CompanyName { get; set; }   
    [Column("inn")] public string INN { get; set; }   
    [Column("phone_number")] public string PhoneNumber { get; set; }   
    [Column("password")] public string Password { get; set; }   
    [Column("IsSigned")] public bool IsSigned { get; set; }
     
}