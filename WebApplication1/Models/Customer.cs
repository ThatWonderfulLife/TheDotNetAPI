using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("customer", Schema = "public")]
    public class Customer
    {

    [Column("customer_id")]
    public int customerId { get; set; }

    [Column("store_id")]
    public int storeId { get; set; }

    [Column("first_name")]
    public string customerFirstName { get; set; }

    [Column("last_name")]
    public string customerLastName { get; set; }

    [Column("email")]
    public string customerEmail{ get; set; }

    [Column("address_id")]
    public int addressId { get; set; }

    [Column("activebool")]
    public bool activeBool { get; set; }

    [Column("create_date")]
    public DateOnly createDate { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }

    [Column("active")]
    public int isActive { get; set; }

}
