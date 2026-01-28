using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("category", Schema = "public")]
public class Category
{
    [Column("category_id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("last_update")]
    public DateTimeOffset LastUpdate { get; set; }
}

