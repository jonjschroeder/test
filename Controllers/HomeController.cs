using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank.Models;
using System.Linq;

namespace bank.Controllers
{
    public class HomeController : Controller
    {
            private YourContext _context;
 
            public HomeController(YourContext context)
    {
        _context = context;
    }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            ViewBag.Errors = new List<string>();
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model){
            System.Console.WriteLine("In Register***********************************************");
            if (ModelState.IsValid){
                User NewUser = new User{
                    first_name = model.first_name,
                    last_name = model.last_name,
                    email = model.email,
                    password = model.password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("CurrentUserId", NewUser.IdUsers);
                return RedirectToAction("Dashboard");
            }else{
                System.Console.WriteLine("Not Good***********************************************");
                ViewBag.Errors = ModelState.Values;
            }
            return View("index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User CurrUser = _context.Users.Where(e => e.email == email).SingleOrDefault();
             if (CurrUser != null){
                if (password == CurrUser.password){
                    HttpContext.Session.SetInt32("CurrentUserId", CurrUser.IdUsers);
                    return RedirectToAction("Dashboard");  
               }
             }
             ViewBag.LoginError = "Invalid combination. Please provide a valid username/password.";
             return View("Index");              
       }
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard(){
            ViewBag.CurrentUser = _context.Users.SingleOrDefault(user => user.IdUsers == HttpContext.Session.GetInt32("CurrentUserId"));
            if(HttpContext.Session.GetString("NoMoneyError") == null){
                ViewBag.NoMoneyError = "";
            }else{
                ViewBag.NoMoneyError = HttpContext.Session.GetString("NoMoneyError");
            }
            ViewBag.AllMyTransactions = _context.Transactions.Where(Action => Action.UserId == HttpContext.Session.GetInt32("CurrentUserId"));
            return View("Dashboard");
        }
    }
}
