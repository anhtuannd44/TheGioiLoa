using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheGioiLoa.Models.ViewModel
{
    public class JsonStatusViewModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string partial { get; set; }
    }
}