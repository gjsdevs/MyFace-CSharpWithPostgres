using System.Web.Mvc;
using System.Web.Security;
using MyFace.DataAccess;
using MyFace.Models.ViewModels;

namespace MyFace.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userToLogIn)
        {
            User userToCheckAgainst = userRepository.GetSingleUser(userToLogIn.username);
                bool IsValidUser = userToLogIn.username.ToLower() == userToCheckAgainst.username && userToLogIn.password == userToCheckAgainst.password;
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(userToLogIn.username, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "invalid Username or Password");
                return View();
            
        }

        public ActionResult SignUp()
        {
            return View( new LoginViewModel());
        }

        [HttpPost]
        public ActionResult SignUp(LoginViewModel loginViewModel)
        {
            string username = loginViewModel.Username.ToLower();

            userRepository.CreateUser(new User() { username = username, password = loginViewModel.Password, fullname = loginViewModel.FullName });

            //TODO Implement signing up
            ViewBag.LoginMessage = "Thank you for signing up, please log in";
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}