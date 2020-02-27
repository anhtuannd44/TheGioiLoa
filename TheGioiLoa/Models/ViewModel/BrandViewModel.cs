using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class CreateBrandViewModel
    {
        [Display(Name = "Tên thương hiệu bạn muốn tạo?")]
        public string Name { get; set; }
    }

    public class BrandViewModel
    {
        public List<Brand> BrandList { get; set; }

        public string Notification { get; set; }
    }
    public class BrandSelectedViewModel
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}