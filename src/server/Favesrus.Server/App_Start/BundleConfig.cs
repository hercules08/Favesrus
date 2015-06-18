using System.Web.Optimization;

namespace Favesrus.Server
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themeCss").Include(

                //CSS Global Compulsory
                "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                "~/assets/css/one.style.css",

                //CSS Footer
                "~/assets/css/footers/footer-v7.css",

                //CSS Implementing Plugins
                "~/assets/plugins/animate.css",
                "~/assets/plugins/line-icons/line-icons.css",
                "~/assets/plugins/font-awesome/css/font-awesome.min.css",
                "~/assets/plugins/pace/pace-flash.css",
                "~/assets/plugins/owl-carousel/owl.carousel.css",
                "~/assets/plugins/cube-portfolio/cubeportfolio/css/cubeportfolio.min.css",
                "~/assets/plugins/cube-portfolio/cubeportfolio/custom/custom-cubeportfolio.css",
                "~/assets/plugins/revolution-slider/rs-plugin/css/settings.css",

                //CSS Theme
                "~/assets/css/theme-skins/one.dark.css",

                //CSS Customization
                "~/assets/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/themeJS").Include(
                //JS Global Compulsory
                "~/assets/plugins/jquery/jquery-migrate.min.js",
                "~/assets/plugins/bootstrap/js/bootstrap.min.js",

                //JS Implementing Plugins
                "~/assets/plugins/smoothScroll.js",
                "~/assets/plugins/jquery.easing.min.js",
                "~/assets/plugins/pace/pace.min.js",
                "~/assets/plugins/jquery.parallax.js",
                "~/assets/plugins/counter/waypoints.min.js",
                "~/assets/plugins/counter/jquery.counterup.min.js",
                "~/assets/plugins/owl-carousel/owl.carousel.js",
                "~/assets/plugins/sky-forms-pro/skyforms/js/jquery.form.min.js",
                "~/assets/plugins/sky-forms-pro/skyforms/js/jquery.validate.min.js",
                "~/assets/plugins/revolution-slider/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/assets/plugins/revolution-slider/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/assets/plugins/cube-portfolio/cubeportfolio/js/jquery.cubeportfolio.min.js",

                //JS Page Level
                "~/assets/js/one.app.js",
                "~/assets/js/forms/login.js",
                "~/assets/js/forms/contact.js",
                "~/assets/js/plugins/pace-loader.js",
                "~/assets/js/plugins/owl-carousel.js",
                "~/assets/js/plugins/revolution-slider.js",
                "~/assets/js/plugins/cube-portfolio/cube-portfolio-lightbox.js"
            ));
        }
    }
}