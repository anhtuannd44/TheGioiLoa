using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class ContactViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email{ get; set; }

        [Display(Name = "Hotline")]
        [Phone(ErrorMessage = "Chưa đúng định dạng Số điện thoại")]
        public string Hotline { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }

    public class SocialViewModel
    {
        [Display(Name = "Facebook (Url Fanpage)")]
        [Url(ErrorMessage = "Chưa đúng định dạng URL")]
        public string Facebook { get; set; }

        [Display(Name = "Youtube (Url Video)")]
        [Url(ErrorMessage = "Chưa đúng định dạng URL")]
        public string Youtube { get; set; }

        [Display(Name = "Zalo (Id Zalo App)")]
        public string Zalo { get; set; }
    }

    public class FooterViewModel
    {
        public List<Menu> Footer1 { get; set; }
        public List<Menu> Footer2 { get; set; }
        public List<Menu> Footer3 { get; set; }
    }
    public class SocialLinkViewModel
    {
        public string Link { get; set; }
    }
    public class FooterContactViewModel
    {
        [AllowHtml]
        public string FooterContact { get; set; }
        [AllowHtml]
        public string FooterCopyright {get;set;}
    }
}