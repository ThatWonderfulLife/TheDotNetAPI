using System.ComponentModel.DataAnnotations.Schema;
namespace PostgresAPI.Models;

[Table("sensor_data", Schema = "public")]
public class Sensor
{
    [Column("id")]
    public int Id { get; set; }

    [Column("sensor_name")]
    public string SensorName { get; set; }

    [Column("temperature")]
    public float Temperature { get; set; }

    [Column("humidity")]
    public float Humidity { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}

