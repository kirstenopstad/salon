namespace HairSalon.Models
{
  public class Appointment
  {
    // properties
    public int AppointmentId {get; set;}
    public string DateTime { get; set;}
    // foreign keys
    public int StylistId {get; set;}
    public int ClientId {get; set;}
    // reference navigation properties
    public Stylist Stylist {get; set;}
    public Client Client {get; set;}
  }
}
