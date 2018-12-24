using System.Web;
using System.Web.Optimization;

namespace Metropole
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap 4
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Surveys - not currently used.
            bundles.Add(new ScriptBundle("~/bundles/cleditor")
                           .Include("~/Scripts/jquery.cleditor.js")
                           .Include("~/Scripts/knockout.cleditor.js"));

            // Used to build the survey UI.
            bundles.Add(new ScriptBundle("~/bundles/knockout")
                            .Include("~/Scripts/knockout-{version}.js")
                            .Include("~/Scripts/knockout.mapping-latest.js")
                            .Include("~/Scripts/knockout.enter.js")
                            .Include("~/Scripts/knockout.validation.js"));

            // Provides Toast Notifications to indicate successful saves on submits - the notification
            // appears on the next page redirected to after a save.
            bundles.Add(new ScriptBundle("~/bundles/toastr")
                            .Include("~/Scripts/toastr.js"));

            //Knockout view models.
            bundles.Add(new ScriptBundle("~/bundles/models")
                            .Include("~/Scripts/vm.question.js")
                            .Include("~/Scripts/vm.survey.js")
                            .Include("~/Scripts/vm.surveylist.js")
                            .Include("~/Scripts/vm.history.js"));


            // Basic bootstrap theme - some overrides for the menu to prevent it going full
            // screen width.
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/less/bootstrap-overrides.css",
                     "~/Content/themes/base/datepicker.css",
                      "~/Content/site.css",
                      "~/Content/less/debug.css"));


            // Survey Styles
            bundles.Add(new StyleBundle("~/Content/cleditor")
                           .Include("~/Content/jquery.cleditor.css"));

            // Styling for Toast Notifications.
            bundles.Add(new StyleBundle("~/Content/toastr")
                            .Include("~/Content/toastr.css"));

        }
    }
}
