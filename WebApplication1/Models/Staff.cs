using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("staff", Schema = "public")]
public class Staff
    {

    [Column("staff_id")]
    public int staffId { get; set; }

    [Column("first_name")]
    public string staffFirstName { get; set; }

    [Column("last_name")]
    public string staffLastName { get; set; }

    [Column("address_id")]
    public int staffAddressId { get; set; }

    [Column("email")]
    public string staffEmail { get; set; }

    [Column("store_id")]
    public int staffStoreId { get; set; }

    [Column("active")]
    public bool staffActive { get; set; }

    [Column("username")]
    public string staffUsername { get; set; }

    [Column("password")]
    public string staffPassword { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }

    [Column("picture")]
    public byte[] staffPicture{ get; set; }




}
