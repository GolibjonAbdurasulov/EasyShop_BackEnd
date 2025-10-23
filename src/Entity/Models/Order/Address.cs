using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Order
{
    [Table("addresses")]
    public class Address : ModelBase<long>
    {
        // 🏠 To‘liq manzil (foydalanuvchi kiritgan matn)
        [Column("full_address")]
        public string FullAddress { get; set; }

        // 🌍 Geolokatsiya koordinatalari
        [Column("latitude")]
        public double? Latitude { get; set; }   // kenglik (masalan: 41.2995)

        [Column("longitude")]
        public double? Longitude { get; set; }  // uzunlik (masalan: 69.2401)

        // 🏙 Shahar va mintaqa ma’lumotlari
        [Column("city")]
        public string? City { get; set; }

        [Column("region")]
        public string? Region { get; set; }

        [Column("postal_code")]
        public string? PostalCode { get; set; }
        
        [Column("client_id"),ForeignKey(nameof(Client))]
        public long ClientId { get; set; }

        public virtual Client.Client Client { get; set; }
    }
}

