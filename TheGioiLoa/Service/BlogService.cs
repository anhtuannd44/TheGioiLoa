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

    public class BlogService
    {
        private TheGioiLoaModel db = new TheGioiLoaModel();
        private HelperFunction _helper = new HelperFunction();

        //Type: 1-Blog 2-Page
        public void CreateBlog(Blog blog, int type)
        {
            blog.Title = _helper.DeleteSpace(blog.Title);
            blog.Url = _helper.CreateUrl(blog.Title);
            blog.Type = type;
            blog.DateCreated = DateTime.Now;
            blog.DateModified = DateTime.Now;
            db.Blog.Add(blog);
            db.SaveChanges();
        }

        public BlogViewModel GetBlog(Blog blog)
        {
            var model = new BlogViewModel()
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                BlogContent = blog.BlogContent,
                Status = blog.Status,
                BlogCategoryId = blog.BlogCategoryId,
                Type = blog.Type,
                Cover = blog.Cover,
                StatusLabel = GetStatus(blog.Status),
                Description = blog.Description
            };
            var categories = db.BlogCategory.ToList();
            model.CategoryList = categories;
            model.StatusList = GetStatus();
            return model;
        }

        //type: 1-Blog 2-Page
        public List<BlogViewModel> GetBlogList(string status, int type)
        {
            var blogList = db.Blog.Where(a => a.Type == type).ToList();

            switch (status)
            {
                case "All":
                    break;
                case "Public":
                    blogList = blogList.Where(a => a.Status == 1).ToList();
                    break;
                case "Private":
                    blogList = blogList.Where(a => a.Status == 2).ToList();
                    break;
                default:
                    blogList = null;
                    break;
            }
            var result = new List<BlogViewModel>();
            var blogCategory = db.BlogCategory;
            foreach (var item in blogList)
            {
                var addBlog = new BlogViewModel()
                {
                    BlogId = item.BlogId,
                    Title = item.Title,
                    Url = item.Url,
                    BlogCategoryId = item.BlogCategoryId,
                    Status = item.Status,
                    DateCreated = item.DateCreated,
                    StatusLabel = GetStatus(item.Status),
                    Cover = item.Cover,
                    BlogContent =item.BlogContent,
                    Description = item.Description,
                    BlogCategory= item.BlogCategory
                };
                result.Add(addBlog);
            }
            return result;
        }

        public void EditBlog(Blog blog)
        {
            blog.Title = _helper.DeleteSpace(blog.Title);
            blog.Url = _helper.CreateUrl(blog.Title);
            blog.DateModified = DateTime.Now;
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();
        }
        public List<StatusEnum> GetStatus()
        {
            var statusList = new List<StatusEnum>(){
            new StatusEnum(){StatusId = 1, Name = "Công bố"},
            new StatusEnum(){StatusId = 2, Name = "Chưa đăng"}
            };
            return statusList;
        }
        public void CreateBlogCategory(string blogCategoryName)
        {
            blogCategoryName = _helper.DeleteSpace(blogCategoryName);
            var addItem = new BlogCategory()
            {
                Name = blogCategoryName,
                Url = _helper.CreateUrl(blogCategoryName),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            db.BlogCategory.Add(addItem);
            db.SaveChanges();
        }
        public void EditBlogCateogry(int blogCategoryId, string Name)
        {
            Name = _helper.DeleteSpace(Name);
            var editItem = db.BlogCategory.Find(blogCategoryId);
            editItem.Name = Name;
            editItem.Url = _helper.CreateUrl(Name);
            editItem.DateModified = DateTime.Now;
            db.Entry(editItem).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteBlogCategory(int blogCategoryId)
        {
            var blogInCategory = db.Blog.Where(a => a.BlogCategoryId == blogCategoryId).ToList();
            if (blogInCategory != null)
            {
                foreach (var item in blogInCategory)
                {
                    var editBlog = db.Blog.Find(item.BlogId);
                    editBlog.BlogCategoryId = null;
                    db.Entry(editBlog).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            db.BlogCategory.Remove(db.BlogCategory.Find(blogCategoryId));
            db.SaveChanges();
        }
        public string GetStatus(int status)
        {
            if (status == 1)
                return "Đã đăng";
            else
                return "Chưa đăng";
        }
    }
}