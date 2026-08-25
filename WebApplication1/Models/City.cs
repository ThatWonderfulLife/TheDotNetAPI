using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace PostgresAPI.Models;

[Table("city", Schema = "public")]
public class City
{
    [Column("city_id")]
    public int Id { get; set; }

    [Column("city")]
    public string CityName { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("last_update")]
    public DateTimeOffset LastUpdate { get; set; }
}

