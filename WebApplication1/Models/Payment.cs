using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("payment", Schema = "public")]
public class Payment
    {

    [Column("payment_id")]
    public int paymentId { get; set; }

    [Column("customer_id")]
    public int paymentCustomerId { get; set; }

    [Column("staff_id")]
    public int paymentStaffId { get; set; }

    [Column("rental_id")]
    public int paymentRentalId { get; set; }

    [Column("ammount")]
    public decimal paymentAmmount{ get; set; }

    [Column("payment_date")]
    public DateTimeOffset paymentDate { get; set; }
}

