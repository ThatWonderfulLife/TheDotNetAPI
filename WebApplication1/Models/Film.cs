using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("film", Schema = "public")]
public class Film
    {
    [Column("film_id")]
    public int filmId { get; set; }

    [Column("title")]
    public string filmTitle { get; set; }

    [Column("description")]
    public string filmDescription { get; set; }

    [Column("release_year")]
    public int filmReleaseYear { get; set; }

    [Column("language_id")]
    public int filmLanguage { get; set; }

    [Column("original_language_id")]
    public int filmOriginalLanguage { get; set; }

    [Column("rental_duration")]
    public int filmRentalDuration { get; set; }

    [Column("rental_rate")]
    public decimal filmRentalRate { get; set; }

    [Column("length")]
    public int filmLength { get; set; }

    [Column("replacement_cost")]
    public decimal filmReplacementCost { get; set; }

    [Column("rating")]
    public string filmRating { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }

    [Column("special_features")]
    public string[] filmSpecialFeatures { get; set; }

    [Column("fulltext")]
    public NpgsqlTsVector filmFullText { get; set; }
}
