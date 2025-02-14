using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using learn_dotnet.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using learn_dotnet.Data;

namespace learn_dotnet.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly PizzashopCContext _context;

    public LoginController(ILogger<LoginController> logger, PizzashopCContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        Console.WriteLine(model.username);
        Console.WriteLine(model.password);
        Console.WriteLine(model.IsChecked);
        if (ModelState.IsValid)
        {
            Console.WriteLine("model valid");
            
            var lgr = _context.Logins.FirstOrDefault(l => l.Email == model.username && l.Password == model.password);
            if (lgr != null)
            {
                if (model.IsChecked)
                {
                    
                    HttpContext.Session.SetString("username", lgr.Email);
                    HttpContext.Session.SetString("password", lgr.Password);
                }
                return RedirectToAction("Index","Dashboard");
            }
            else
            {
                Console.WriteLine("invalid user");
                ViewBag.ErrorMessage = "Email not found or matched";
                ModelState.AddModelError("Username", "invalid Email address");
                ModelState.AddModelError("password", "invalid password");
                return View(model);
            }
        }
        return View(model);
    }
    
    // public IActionResult Loginuser(bool isChecked, string username, string password)
    // {
    //     if (isChecked)
    //     {
    //         HttpContext.Session.SetString("username", username);
    //         HttpContext.Session.SetString("password", password);
    //     }

    //     return RedirectToAction("ForgetPass");
    // }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ForgetPass()
    {
        Console.WriteLine(HttpContext.Session.GetString("username"));
        Console.WriteLine(HttpContext.Session.GetString("password"));


        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
