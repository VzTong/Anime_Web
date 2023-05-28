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
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ và Tên không hợp lệ")]
        [StringLength(50, MinimumLength = 3)]
        public string Fullname_user { get; set; }

        [Required(ErrorMessage = "Tên tài khoản không hợp lệ")]
        [StringLength(50, MinimumLength = 3)]
        public string User_name { get; set; }

        [Required(ErrorMessage = "Số điện thoại không hợp lệ không hợp lệ")]
        [RegularExpression(@"^\+*\d{10,15}$")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        //[RegularExpression(@"^{6,15}$")]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Mật khẩu không khớp")]
        [Compare("Password")]
        public string F_Password { get; set; }
    }
}
