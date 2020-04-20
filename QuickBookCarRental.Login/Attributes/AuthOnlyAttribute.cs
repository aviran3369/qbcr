using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace QuickBookCarRental.Login.Attributes
{
    public class AuthOnlyAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly Lazy<bool> DevMode = new Lazy<bool>(() => {
            var devMode = WebConfigurationManager.AppSettings["DevMode"];
            return devMode == "1";
        });

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!DevMode.Value)
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}