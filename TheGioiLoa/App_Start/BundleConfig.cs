using System.Web;
using System.Web.Optimization;

namespace TheGioiLoa
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap-jquery/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/bootstrap-jquery/jquery-{version}.js"));

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
                        "~/Scripts/Home/js/main.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.


            //Css Home
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/Home/lib/font-awesome/css/font-awesome.min.css",
                      "~/Content/Home/lib/animate/animate.min.css",
                      "~/Content/Home/lib/ionicons/css/ionicons.min.css",
                      "~/Content/Home/lib/owlcarousel/assets/owl.carousel.min.css",
                      "~/Content/Home/lib/lightbox/css/lightbox.min.css",
                      "~/Content/Home/css/style.css",
                      "~/Content/site.css"));
        }
    }
}
