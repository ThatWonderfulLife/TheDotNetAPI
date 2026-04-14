using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("language", Schema = "public")]

public class Language
    {

    [Column("language_id")]
    public int languageId { get; set; }

    [Column("name")]
    public int languageName { get; set; }

    [Column("last_update")]
    public int lastUpdate { get; set; }

}

