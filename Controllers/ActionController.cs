using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bank.Models;
using System.Linq;

namespace bank.Controllers
{
    public class ActionController : Controller
    {
            private YourContext _context;
 
            public ActionController(YourContext context)
    {
        _context = context;
    }
        // POST: 
        [HttpPost]
        [Route("process")]
        public IActionResult process(int number){
            User thisuser = _context.Users.SingleOrDefault(user => user.IdUsers == HttpContext.Session.GetInt32("CurrentUserId"));
            if(number < 0){ 
                if(-1 *(number) > thisuser.balance){
                    HttpContext.Session.SetString("NoMoneyError", "You do not have enough money to withdrawl");
                    return RedirectToAction("Dashboard", "Home");
                 }
            }
            bank.Models.Transaction NewAction = new bank.Models.Transaction{
                amount = number,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = (int)HttpContext.Session.GetInt32("CurrentUserId")

            };
                if(number >= 0){
                System.Console.WriteLine("This is a deposit");
            }else{
                System.Console.WriteLine("This is a withdrawl");
            }
            _context.Transactions.Add(NewAction);
            thisuser.balance += number;
            _context.SaveChanges();
            System.Console.WriteLine("You have saved changes ******&*&*&*&*&***&&***&&*&*&*&*&*&*&*&*&*&*&*&*&*&**&*&*&*&*&**&*&");
            
            return RedirectToAction("Dashboard", "Home");
        }
    }
}
