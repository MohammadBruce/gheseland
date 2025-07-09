using System.Web;
using System.Web.Optimization;

namespace gheseland
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/css/libraries").Include(
                      "~/assets/bootstrap.4.3.1/css/bootstrap.min.css",
                      "~/assets/owlCarousel2-2.3.4/assets/owl.carousel.min.css",
                      "~/assets/owlCarousel2-2.3.4/assets/owl.theme.default.min.css",
                      "~/assets/toastr/toastr.css"
              //"~/assets/jquery.mobile/jquery.mobile-1.4.5.min.css"
              ));
            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/assets/bootstrap.4.3.1/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/css/main").Include(
                "~/fonts/font.css",
                "~/css/fontstyle.css",
                "~/css/main.css"));


            bundles.Add(new ScriptBundle("~/js/libraries").Include(
                "~/assets/jquery-3.4.1/jquery-1.11.3.min.js",
                //"~/assets/jquery.mobile/jquery.mobile-1.4.5.min.js",
                "~/assets/toastr/toastr.js",
                "~/assets/bootstrap.4.3.1/js/bootstrap.min.js",
                "~/assets/owlCarousel2-2.3.4/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/js/main").Include(
                "~/js/common/config.js",
                "~/js/common/urlFunctions.js",
                "~/js/common/data.js",
                "~/js/story.js",
                "~/js/player.js",
                "~/js/app.js"));

          bundles.Add(new ScriptBundle("~/js/story").Include(
            "~/js/player.js"));
    }
    }
}
