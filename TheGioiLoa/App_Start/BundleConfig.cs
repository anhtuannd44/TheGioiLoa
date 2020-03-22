﻿using System.Web;
using System.Web.Optimization;

namespace TheGioiLoa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Admin/plugins/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Admin/plugins/jquery/jquery.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/bootstrap-jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jshome").Include(
                        "~/Scripts/Home/js/easing/easing.min.js",
                        "~/Scripts/Home/js/mobile-nav/mobile-nav.js",
                        "~/Scripts/Home/js/wow/wow.min.js",
                        "~/Scripts/Home/js/waypoints/waypoints.min.js",
                        "~/Scripts/Home/js/counterup/counterup.min.js",
                        "~/Content/Home/lib/owlcarousel/owl.carousel.min.js",
                        "~/Scripts/Home/js/isotope/isotope.pkgd.min.js",
                        "~/Content/Home/lib/lightbox/js/lightbox.min.js",
                        "~/Scripts/Home/js/contactform/contactform.js",
                        "~/Content/Admin/plugins/toastr/toastr.min.js",
                        "~/Content/Admin/plugins/ion-rangeslider/js/ion.rangeSlider.min.js",
                        "~/Content/rating/bootstrap-rating.min.js",
                        "~/Scripts/Home/js/main.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/AdminJs").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Content/Admin/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                      "~/Content/Admin/plugins/jquery-mousewheel/jquery.mousewheel.js",
                      "~/Content/Admin/plugins/raphael/raphael.min.js",
                      "~/Content/Admin/plugins/jquery-mapael/jquery.mapael.min.js",
                      "~/Content/Admin/plugins/jquery-mapael/maps/usa_states.min.js",
                      "~/Content/Admin/plugins/chart.js/Chart.min.js",
                      "~/Content/Admin/dist/js/pages/dashboard2.js",
                      "~/Content/Admin/plugins/ekko-lightbox/ekko-lightbox.min.js",
                      "~/Content/Admin/plugins/toastr/toastr.min.js",
                       "~/Scripts/Admin/adminlte.min.js",
                      "~/Content/Admin/plugins/summernote/summernote-bs4.min.js",
                      "~/Content/Admin/plugins/upload/dist/js/jquery.dm-uploader.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.


            bundles.Add(new StyleBundle("~/Content/BootstrapCss").Include(
                     "~/Content/bootstrap/bootstrap.css"));

            //Css Home
            bundles.Add(new StyleBundle("~/Content/HomeCss").Include(
                       "~/Content/Home/lib/animate/animate.min.css",
                       "~/Content/Home/lib/ionicons/css/ionicons.min.css",
                       "~/Content/Home/lib/owlcarousel/assets/owl.carousel.min.css",
                       "~/Content/Home/lib/lightbox/css/lightbox.min.css",
                       "~/Content/Home/css/style.css",
                       "~/Content/Home/slider-product/dist/styles/vit-gallery.css",
                       "~/Content/Admin/plugins/ion-rangeslider/css/ion.rangeSlider.min.css",
                       "~/Content/Admin/plugins/toastr/toastr.min.css",
                       "~/Content/rating/bootstrap-rating.css",
                       "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                      "~/Content/Admin/css/adminlte.css",
                      "~/Content/Admin/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                      "~/Content/Admin/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/Admin/plugins/toastr/toastr.min.css",
                      "~/Content/Admin/plugins/summernote/summernote-bs4.css",
                      "~/Content/Admin/plugins/upload/dist/css/jquery.dm-uploader.min.css",
                      "~/Content/Admin/SiteAdmin.css"
                      ));
        }
    }
}
