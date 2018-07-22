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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Surveys
            bundles.Add(new ScriptBundle("~/bundles/cleditor")
                           .Include("~/Scripts/jquery.cleditor.js")
                           .Include("~/Scripts/knockout.cleditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout")
                            .Include("~/Scripts/knockout-{version}.js")
                            .Include("~/Scripts/knockout.mapping-latest.js")
                            .Include("~/Scripts/knockout.enter.js")
                            .Include("~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr")
                            .Include("~/Scripts/toastr.js"));

            //TBD: vm.report.js is it used?
            bundles.Add(new ScriptBundle("~/bundles/models")
                            .Include("~/Scripts/vm.department.js")
                            .Include("~/Scripts/vm.question.js")
                            .Include("~/Scripts/vm.responselist.js")
                            .Include("~/Scripts/vm.survey.js")
                            .Include("~/Scripts/vm.surveylist.js"));





            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/less/bootstrap-overrides.css",
                     "~/Content/themes/base/datepicker.css",
                      "~/Content/site.css",
                      "~/Content/less/debug.css"));


            // Survey Styles
            bundles.Add(new StyleBundle("~/Content/cleditor")
                           .Include("~/Content/jquery.cleditor.css"));

            bundles.Add(new StyleBundle("~/Content/toastr")
                            .Include("~/Content/toastr.css"));

        }
    }
}
