using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("actor", Schema = "public")]
public class Actor
    {
        [Column("actor_id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("last_update")]
        public DateTimeOffset LastUpdate{ get; set; }
    }

