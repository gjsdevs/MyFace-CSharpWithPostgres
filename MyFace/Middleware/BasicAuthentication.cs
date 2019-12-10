using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFace.DataAccess;
using MyFace.Helpers;

namespace MyFace.Middleware
{
    //TODO Replace basic authentication with a better authentication method.
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        UserRepository userRepo = new UserRepository();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var userNameAndPassword = AuthenticationHelper.ExtractUsernameAndPassword(req);

            if (userNameAndPassword != null)
            {
                //TODO get password from the database.
                string username = userNameAndPassword.Username.ToLower();
                string thePassword = userRepo.GetPassword(username);
                if (userNameAndPassword.Password == thePassword) return;
            }
            const string realm = "MyFace";

            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", $"Basic realm=\"{realm}\"");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}