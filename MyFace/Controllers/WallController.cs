using System.Collections;
using System.Web.Mvc;
using MyFace.DataAccess;
using MyFace.Helpers;
using MyFace.Middleware;
using MyFace.Models.ViewModels;

namespace MyFace.Controllers
{
    [BasicAuthentication]
    public class WallController : Controller
    {
        private readonly IPostRepository postRepository;

        private readonly IUserRepository userRepository;

        public WallController(IPostRepository postRepository, IUserRepository userRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }


         

        // GET: Wall
        public ActionResult Index(string username)
        {
            User user = new User();   
            user = userRepository.GetSingleUser(username); 
            var posts = postRepository.GetPostsOnWall(user.username); //gets posts on wall by username

            foreach (var post in posts) //converts usernames on posts to fullnames on posts before displaying them.
            {
                User receiver = userRepository.GetSingleUser(post.Recipient);
                post.Recipient = receiver.fullname;
                User sender = userRepository.GetSingleUser(post.Sender);
                post.Sender = sender.fullname;
            }
            var viewModel = new WallViewModel(posts, user);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NewWall(WallViewModel wallViewModel)
        {
            string username = User?.Identity?.Name; 
            User user = new User();
            User receiver = new User();
            //get user object from repositry by username to display user.fullname
            user = userRepository.GetSingleUser(username);
            receiver = userRepository.GetSingleUser(wallViewModel.OwnerUsername);
            
            postRepository.CreatePost(new Post() { Content = wallViewModel.NewPost, Recipient = receiver.username, Sender = user.username });
            return RedirectToAction("Index", new {username= wallViewModel.OwnerUsername});
        }
    }
}