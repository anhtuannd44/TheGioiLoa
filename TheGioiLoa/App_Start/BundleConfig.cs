using System.Web;
using System.Web.Optimization;

namespace TheGioiLoa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //JS
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-jquery").Include(
                        "~/Scripts/bootstrap-jquery/jquery-3.4.1.min.js",
                        "~/Scripts/bootstrap-jquery/umd/popper.min.js",
                        "~/Content/lazy-loading/jquery.lazy.min.js",
                        "~/Scripts/bootstrap-jquery/bootstrap.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Content/datetimepicker/js/bootstrap-datetimepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/bootstrap-jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jshome").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Content/toastr/toastr.min.js",
                        "~/Content/Admin/plugins/ion-rangeslider/js/ion.rangeSlider.min.js",
                        "~/Content/rating/bootstrap-rating.min.js",
                        "~/Content/Home/slider-product/dist/scripts/vit-gallery.js",
                        "~/Content/Home/slider-product/main.js",
                        "~/Scripts/Home/js/easing/easing.min.js",
                        "~/Scripts/Home/js/main.js",
                        "~/Scripts/loading-spinner.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/manage").Include(
                        "~/Scripts/Home/js/manage.js"));
            bundles.Add(new ScriptBundle("~/bundles/product").Include(
                       "~/Scripts/Home/Js/product.js"));

            bundles.Add(new ScriptBundle("~/bundles/AdminJs").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Content/toastr/toastr.min.js",
                      "~/Scripts/Admin/adminlte.min.js",
                      "~/Content/Admin/plugins/summernote/summernote-bs4.js",
                      "~/Content/Admin/plugins/upload/dist/js/jquery.dm-uploader.min.js",
                      "~/Scripts/Admin/ImageUpload/upload-img.js",
                      "~/Scripts/Admin/summnernote.js",
                      "~/Scripts/loading-spinner.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/admin/information").Include(
                        "~/Scripts/Admin/information.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/product").Include(
                        "~/Scripts/Admin/product.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/blog").Include(
                        "~/Scripts/Admin/blog.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/menutop").Include(
                        "~/Scripts/Admin/menu-top.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/review").Include(
                        "~/Scripts/Admin/review.js"));
            bundles.Add(new ScriptBundle("~/bundles/admin/category").Include(
                        "~/Scripts/Admin/category.js"));
            

            //CSS
            bundles.Add(new StyleBundle("~/Content/BootstrapCss").Include(
                     "~/Content/datetimepicker/css/bootstrap-datetimepicker.min.css",
                     "~/Content/bootstrap/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/onepagecss").Include(
                       "~/Content/Home/css/onepage.css"));
            //Css Home
            bundles.Add(new StyleBundle("~/Content/HomeCss").Include(
                       "~/Content/FontAwesome/css/all.css",
                       "~/Content/Home/slider-product/dist/styles/vit-gallery.css",
                       "~/Content/Admin/plugins/ion-rangeslider/css/ion.rangeSlider.min.css",
                       "~/Content/toastr/toastr.css",
                       "~/Content/rating/bootstrap-rating.css",
                       "~/Content/PagedList.css",
                       "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                      "~/Content/FontAwesome/css/all.css",
                      "~/Content/Admin/css/adminlte.css",
                      "~/Content/Admin/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                      "~/Content/Admin/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/toastr/toastr.css",
                      "~/Content/Admin/plugins/summernote/summernote-bs4.css",
                      "~/Content/Admin/plugins/upload/dist/css/jquery.dm-uploader.min.css",
                      "~/Content/PagedList.css",
                      "~/Content/Admin/SiteAdmin.css"
                      ));
        }
    }
}
