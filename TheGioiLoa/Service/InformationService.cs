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

    public class InformationService
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _helper = new HelperFunction();

        public ContactViewModel GetContact()
        {
            var information = db.Information.Find("Main");
            return new ContactViewModel()
            {
                Address = information.Address,
                Email = information.Email,
                Hotline = information.Hotline
            };
        }
        public SocialViewModel GetSocial()
        {
            var information = db.Information.Find("Main");
            return new SocialViewModel()
            {
                Facebook = information.Facebook,
                Youtube = information.Youtube,
                Zalo = information.Zalo
            };
        }
        public void CreateInformation()
        {
            var item = new Information()
            {
                Id = "Main",
                Logo = "No_Picture.JPG"
            };
            db.Information.Add(item);
            db.SaveChanges();
        }
        public void UpdateLogo(string logo)
        {
            var item = db.Information.Find("Main");
            item.Logo = logo;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void UpdateContact(ContactViewModel contact)
        {
            var item = db.Information.Find("Main");
            item.Email = contact.Email;
            item.Address = contact.Address;
            item.Hotline = contact.Hotline;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void UpdateSocial(SocialViewModel social)
        {
            var item = db.Information.Find("Main");
            item.Facebook = social.Facebook;
            item.Youtube = social.Youtube;
            item.Zalo = social.Zalo;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }


        //MenuTop: 0; Footer [1 2 3] - [1 2 3]
        public List<Menu> GetMenuList(int type)
        {
            return db.Menu.Where(a => a.Type == type).ToList();
        }
        public Menu GetMenu(int menuId)
        {
            return db.Menu.Find(menuId);
        }
        public void AddMenu(Menu menu)
        {
            db.Menu.Add(menu);
            db.SaveChanges();
        }
        public void EditMenu(Menu menu)
        {
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteMenu(int menuId)
        {
            db.Menu.Remove(db.Menu.Find(menuId));
            db.SaveChanges();
        }
        public SocialLinkViewModel GetSocialLink(string social)
        {
            var information = db.Information.Find("Main");
            if (information == null)
                CreateInformation();
            var socialLink = new SocialLinkViewModel();
            if (social == "Youtube" && !string.IsNullOrEmpty(information.Youtube))
            {
                socialLink.Link = _helper.GetYoutubeVideoId(information.Youtube);
            }
            else if (social == "Facebook")
                socialLink.Link = information.Facebook;
            else if (social == "Zalo")
                socialLink.Link = information.Zalo;
            return socialLink;
        }
        public List<Slider> GetSliderImageList()
        {
            return db.Slider.ToList();
        }
        public void AddImageToSlider(Slider slider)
        {
            db.Slider.Add(slider);
            db.SaveChanges();
        }
        public void DeleteSlider(int sliderId)
        {
            db.Slider.Remove(db.Slider.Find(sliderId));
            db.SaveChanges();
        }
        public FooterContactViewModel GetFooterContact()
        {
            var information = db.Information.Find("Main");
            return new FooterContactViewModel()
            {
                FooterContact = information.FooterContact,
                FooterCopyright = information.FooterCopyright
            };
        }
        public void UpdateFooterContact(string footerContact, string footerCopyright)
        {
            var item = db.Information.Find("Main");
            item.FooterContact =  footerContact;
            item.FooterCopyright = footerCopyright;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}