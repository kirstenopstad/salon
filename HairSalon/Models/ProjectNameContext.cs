using Microsoft.EntityFrameworkCore;

namespace ProjectName.Models
{
  public class ProjectNameContext : DbContext
  {
    // include DbSets as needed

    public ProjectNameContext(DbContextOptions options) : base(options) { }
  }
}