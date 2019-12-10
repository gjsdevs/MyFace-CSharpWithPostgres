using System.Web.Mvc;
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
            return RedirectToAction("Index","UserList");
        }
    }
}