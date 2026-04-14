using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("country", Schema = "public")]
public class Country
{
    [Column("country_id")]
    public int Id { get; set; }

    [Column("country")]
    public string countryName { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }

}

