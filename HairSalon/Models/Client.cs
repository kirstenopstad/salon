namespace HairSalon.Models
{
  public class Client
  {
    // properties
    public int ClientId {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Phone {get; set;}
    public string Email {get; set;}
    // foreign key
    public int StylistId {get; set;}
    // reference navigation property
    public Stylist Stylist {get; set;}
  }
}
