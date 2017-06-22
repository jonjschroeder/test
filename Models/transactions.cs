using System;
using System.ComponentModel.DataAnnotations;
 
namespace bank.Models
{
    public class Transaction
    {
        [Key]
        public int IdTransactions { get; set; }

        [Required]
        [MinLength(3)]

        public int amount { get; set; }
 
        [Required]
        [MinLength(3)]

        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public User User {get; set;}  //placeholder when using UsersId information will be placed here not the database
        
    }
}