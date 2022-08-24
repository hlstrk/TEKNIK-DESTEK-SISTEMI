using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace TeknikServis.MvcUI.App_Start
{
       public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/Css").Include(
                "~/Content/AdminLTE/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                "~/Content/AdminLTE/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                "~/Content/AdminLTE/plugins/jqvmap/jqvmap.min.css",
                "~/Content/AdminLTE/dist/css/adminlte.min.css",
                "~/Content/AdminLTE/Chat.css",
                "~/Content/AdminLTE/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
          
                "~/Content/AdminLTE/plugins/summernote/summernote-bs4.min.css",
                "~/Content/chosen.min.css",
                "~/Content/ionicons.min.css",
                "~/Content/select2.min.css",
                "~/Content/AdminLTE/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                "~/Content/AdminLTE/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                "~/Content/AdminLTE/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                "~/Content/flatpickr.min.css",
                   "~/Content/selectize.bootstrap4.css",
                     "~/Content/bootstrap4-toggle.min.css"










                )
                .Include("~/Content/AdminLTE/plugins/fontawesome-free/css/all.min.css", new CssRewriteUrlTransform()));


            bundles.Add(new StyleBundle("~/bundles/Js").Include(
               "~/Content/AdminLTE/plugins/jquery/jquery.min.js",
                 "~/Content/AdminLTE/plugins/jqvmap/maps/jquery.vmap.usa.js",
               "~/Content/AdminLTE/plugins/jquery-knob/jquery.knob.min.js",
                    "~/Content/AdminLTE/plugins/jqvmap/jquery.vmap.min.js",
               "~/Content/AdminLTE/plugins/bootstrap/js/bootstrap.bundle.min.js",

               "~/Content/AdminLTE/plugins/jquery-ui/jquery-ui.min.js",
                "~/Content/AdminLTE/plugins/datatables/jquery.dataTables.min.js",
               "~/Content/AdminLTE/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                   "~/Content/AdminLTE/dist/js/adminlte.min.js",
               "~/Content/AdminLTE/plugins/chart.js/Chart.min.js",
               "~/Content/AdminLTE/plugins/sparklines/sparkline.js",

        
               "~/Content/AdminLTE/plugins/moment/moment.min.js",
               "~/Content/AdminLTE/plugins/daterangepicker/daterangepicker.js",
               "~/Content/AdminLTE/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
               "~/Content/AdminLTE/plugins/summernote/summernote-bs4.min.js",
               "~/Content/AdminLTE/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
               "~/Content/AdminLTE/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
               "~/Scripts/MyScripts.js",
               "~/Scripts/buttons.bootstrap4.min.js",
               "~/Scripts/buttons.html5.min.js",
               "~/Scripts/dataTables.buttons.min.js",
               "~/Scripts/select2.min.js",
               "~/Scripts/selectize.min.js",
               "~/Scripts/bootstrap4-toggle.min.js",
               "~/Content/AdminLTE/dist/js/pages/dashboard.js",
     
              
                 "~/Scripts/js.cookie.min.js",
              
                 "~/Scripts/flatpickr.js",
                       "~/Scripts/tr.js"








               ));
            //Burası Önemli -> minification yapması için gerekli kod satırı.
           BundleTable.EnableOptimizations = false;    
        }
    }
}
