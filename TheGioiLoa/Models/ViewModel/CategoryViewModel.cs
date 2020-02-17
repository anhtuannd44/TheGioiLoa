using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheGioiLoa.Models.ViewModel
{
    public class CategoryViewModel
    {
        public List<Category> CategoryList { get; set; }

        public string Notification { get; set; }
    }

    public class CategoryItemViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Tên Danh Mục")]
        [Display(Name = "Tên Chuyên Mục")]
        [StringLength(300)]
        public string Name { get; set; }

        public int? CategoryParentId { get; set; }
    }

    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Tên Danh Mục")]
        [Display(Name = "Tên Chuyên Mục")]
        [StringLength(300)]
        public string Name { get; set; }

        public int? CategoryParentId { get; set; }

    }
    public class CategoryListViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public int? CategoryParentId { get; set; }


        public List<CategoryListViewModel> CategoryChildList { get; set; }
    }
    public class EditCategoryViewModel
    {
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Tên Danh Mục")]
        [Display(Name = "Tên Chuyên Mục")]
        [StringLength(300)]
        public string Name { get; set; }

        [Display(Name = "Là danh mục con của?")]
        public bool IsCategoryParentEdit { get; set; }

        public int? CategoryParentId { get; set; }
    }
}