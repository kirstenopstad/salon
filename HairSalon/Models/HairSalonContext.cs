using Microsoft.EntityFrameworkCore;

namespace HairSalon.Models
{
  public class HairSalonContext : DbContext
  {
    DBSet<Client> Clients { get; set; }
    DBSet<Stylist> Stylist { get; set; }

    public HairSalonContext(DbContextOptions options) : base(options) { }
  }
}