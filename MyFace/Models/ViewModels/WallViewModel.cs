using System.Collections.Generic;
using System.Linq;
using MyFace.DataAccess;

namespace MyFace.Models.ViewModels
{
    public class WallViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerFullname { get; set; }
        public string NewPost { get; set; }
        public User CurrentUser { get; set; }

        public WallViewModel() {}

        public WallViewModel(IEnumerable<Post> posts, User user)
        {
            Posts = posts.Select(post => new PostViewModel(post));
            CurrentUser = user;
            OwnerUsername = user.username;
            OwnerFullname = user.fullname;
        }
    }
}