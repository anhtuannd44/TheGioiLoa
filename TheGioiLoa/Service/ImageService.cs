using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;
using TheGioiLoa.Models.ViewModel;

namespace TheGioiLoa.Service
{

    public class ImageService
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _helper = new HelperFunction();

        public void SaveImageInDb(string imageName)
        {
            var image = new Image()
            {
                ImageId = imageName,
                DateCreated = DateTime.Now
            };
            db.Image.Add(image);
            db.SaveChanges();
        }

        public int RemoveImageInDb(string imageName)
        {
            try
            {
                db.Image.Remove(db.Image.Find(imageName));
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public void RemoveImageInServer(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}