using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("film_actor", Schema = "public")]
    public class FilmActor
    {
    [Column("actor_id")]
    public int fmActorId { get; set; }
    [Column("film_id")]
    public int fmFilmId { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }
}
