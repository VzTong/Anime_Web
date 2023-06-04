using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Anime_Web.Models
{
    public class AccountM
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3)]
        public string username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.)|(([\w-]+\.)+))(a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        //                    ErrorMessage = "Please enter valid email.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be more than 6 characters.")]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Password incorrect.")]
        [Compare("password", ErrorMessage = "Please confirm your password!")]
        [DataType(DataType.Password)]
        public string cf_password { get; set; }
    }
}
