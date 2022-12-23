using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    // Routes
    public ActionResult Index()
    {
      return View();
    }

  }
}