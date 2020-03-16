using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class GetFileViewModel
    { 
        public string FileName { get; set; }
    }
    public class ListImageViewModel
    {
        public List<ImageViewModel> ImageList { get; set; }
        public bool IsMultiple { get; set; }
    }
    public class ImageViewModel
    {
        public string ImageId { get; set; }
        public bool IsSelected { get; set; }
    }
}