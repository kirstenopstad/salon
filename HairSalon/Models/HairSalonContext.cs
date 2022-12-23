using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
  public class HairSalonContext : DbContext
  {
    public DBSet<Client> Clients { get; set; }
    public DBSet<Stylist> Stylist { get; set; }

    public HairSalonContext(DbContextOptions options) : base(options) { }
  }
}