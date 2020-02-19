using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheGioiLoa.Models.ViewModel
{
    public class ShowCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int? CategoryParentId { get; set; }

        public List<ShowCategoryViewModel> Children { get; set; }
    }

    public class CategoryViewModel
    {
        public List<Category> CategoryList { get; set; }

        public string Notification { get; set; }
    }

    public class CreateCategoryViewModel
    {
        public string Name { get; set; }

        public int? CategoryParentId { get; set; }

    }
    public class EditCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Tên Chuyên Mục")]
        [StringLength(300)]
        public string Name { get; set; }
    }
    public class RemoveCategoryViewMode
    {
        public int CategoryId { get; set; }
    }
}