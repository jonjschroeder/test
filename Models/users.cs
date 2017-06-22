using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


 
namespace bank.Models
{
    public abstract class BaseEntity {} 
    public class User : BaseEntity
    {
        [Key]
        public int IdUsers { get; set; }  //or put in the[key]

        public string  first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        
        // [Required]
        // [CompareAttribute("Password", ErrorMessage = "Password and Password Confrimation Must match :]")]
        // public string PasswrodConfirmation {get; set;}

        public int balance { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Transaction> Transactions {get; set;}

        public User(){
            Transactions = new List<Transaction>();
            balance = 100; //78 transaction btw 20 ppl query  one user then .include
        }

    }
}