using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db ;

        public HomeController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var game=db.Games.Include(d=>d.Device).ThenInclude(d=>d.device).Include(g=>g.category).ToList();
            return View(game);
        }

  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
