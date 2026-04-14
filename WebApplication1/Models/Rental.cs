using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("rental", Schema = "public")]
public class Rental
    {

    [Column("rental_id")]
    public int rentalId { get; set; }

    [Column("rental_date")]
    public DateTimeOffset rentalDate { get; set; }

    [Column("inventory_id")]
    public int rentalInventoryId { get; set; }

    [Column("customer_id")]
    public int rentalCustomerId { get; set; }

    [Column("return_date")]
    public DateTimeOffset rentalReturnDate { get; set; }

    [Column("staff_id")]
    public int rentalStaffId { get; set; }

    [Column("last_update")]
    public DateTimeOffset lastUpdate { get; set; }
}

