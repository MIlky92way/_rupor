using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
namespace Rupor.Public.App_Start
{
    public class BundleConfig
    {
        private const string styleSrc = "~/Content/glob/scss";
        private const string jsSrc = "~/Content/glob/js";
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css")
                .Include($"{styleSrc}/custom.validate.css"));
        }
    }
}