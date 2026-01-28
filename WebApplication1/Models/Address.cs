using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("address", Schema = "public")]
public class Address
{
    [Column("address_id")]
    public int Id { get; set; }

    [Column("address")]
    public string Address1 { get; set; }

    [Column("address2")]
    public string Address2 { get; set; }

    [Column("district")]
    public string District { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }

    [Column("postal_code")]
    public string PostalCode { get; set; }
    
    [Column("phone")]
    public string Phone { get; set; }

    [Column("last_update")]
    public DateTimeOffset LastUpdate { get; set; }
}

