using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controllers
  {
    // Routes
    public ActionResult Index()
    {
      return View();
    }

  }
}