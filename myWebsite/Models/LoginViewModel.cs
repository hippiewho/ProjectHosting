using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace myWebsite.Models
{

    public class LoginContext : DbContext
    {
        public DbSet<LoginModel> UserList { get; set; }
        
    }

    [Table("Creds")]
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Key]
        [DataType(DataType.Text)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} Must be atleast {2} characters.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do NOT match.")]
        public string ConfirmPassword { get; set; }
    }
}
