using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("film_category", Schema = "public")]
    public class FilmCategory
    {
    [Column("film_id")]
    public int fcFilmId { get; set; }

    [Column("category_id")]
    public int fcCategoryId { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }
}
