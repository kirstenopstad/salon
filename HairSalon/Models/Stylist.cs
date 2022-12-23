using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    // properties
    public int StylistId {get; set;}
    public string Name {get; set;}
    public string Specialty {get; set;}
    public string Bio {get; set;}
    // collection navigation property
    public List<Client> Clients {get; set;}
  }
}
