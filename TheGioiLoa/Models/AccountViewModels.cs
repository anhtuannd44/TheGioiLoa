using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGioiLoa.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Mật Khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ đăng nhập?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Bạn chưa nhập Họ và Tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Bạn chưa nhập Số điện thoại")]
        [Phone(ErrorMessage = "Chưa đúng định dạng Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Mật Khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải từ {2} ký tự trở lên.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận chưa trùng khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
