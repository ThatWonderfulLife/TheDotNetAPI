using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("inventory", Schema = "public")]
    public class Inventory
{
    [Column("inventory_id")]
    public int inventoryId { get; set; }

    [Column("film_id")]
    public int inventoryFilmId { get; set; }

    [Column("store_id")]
    public int inventoryStoreId { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate{ get; set; }
}
