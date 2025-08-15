using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HouseKeeping.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseKeeping.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return View("Index");
    }
    [HttpGet]
    [Route("/housekeepers/{id}")]
    public IActionResult GetHouseKeeper(int id)
    {
        var housekeeper = _dbContext.Housekeepers
            .Include(h => h.JobEntries)
            .FirstOrDefault(h => h.Id == id);
        return View("Housekeeper", housekeeper);
    }
    [HttpGet]
    [Route("/housekeepers")]
    public IActionResult GetAllHousekeepers()
    {
        return View("Housekeepers", _dbContext.Housekeepers.ToList());
    }
    [HttpGet]
    [Route("/login")]
    public IActionResult Login()
    {
        return View("LoginForm");
    }
    [HttpPost]
    [Route("/login")]
    public IActionResult Login(string username, string password)
    {
        var admin = _dbContext.Admins
            .FirstOrDefault(a => a.Username == username && a.Password == password);

        if (admin == null)
        {
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // Store the admin name in session
        HttpContext.Session.SetString("AdminUserName", admin.Username);
        return RedirectToAction("GetAllHousekeepers");
    }
    [HttpPost]
    [Route("/housekeeper/add")]
    public IActionResult AddHousekeeper(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ViewBag.Error = "Housekeeper name cannot be empty.";
            return View("Housekeepers", _dbContext.Housekeepers.ToList());
        }

        var housekeeper = new Housekeeper { Name = name };
        _dbContext.Housekeepers.Add(housekeeper);
        _dbContext.SaveChanges();

        return RedirectToAction("GetAllHousekeepers");
    }
    [HttpGet]
    [Route("/housekeeper/delete/{id}")]
    public IActionResult DeleteHousekeeper(int id)
    {
        var housekeeper = _dbContext.Housekeepers.Find(id);
        if (housekeeper == null)
        {
            return NotFound();
        }

        _dbContext.Housekeepers.Remove(housekeeper);
        _dbContext.SaveChanges();

        return RedirectToAction("GetAllHousekeepers");
    }
    [HttpGet]
    [Route("/rooms/fleetclub1")]
    public IActionResult FleetClub1()
    {
        return View("RoomsFc1");
    }
    [HttpGet]
    [Route("/rooms/fleetclub2")]
    public IActionResult FleetClub2()
    {
        return View("RoomsFc2");
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
