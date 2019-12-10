using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace MyFace.DataAccess
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPostsOnWall(string recipient);
        void CreatePost (Post newPost);

    }

    

    public class PostRepository : IPostRepository
    {
        private readonly IUserRepository userRepository;
        public PostRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<Post> GetPostsOnWall(string recipient)
        {
            IEnumerable<Post> postListToWorkOn = new List<Post>();
            using (var db = ConnectionHelper.CreateSqlConnection())
            {
                
                return db.Query<Post>("SELECT * FROM Posts WHERE recipient = @recipient", new {recipient});
            }

        }

        public void CreatePost(Post newPost)
        {
            //User receivingUser = userRepository.GetSingleUser(newPost.Recipient);
            //User sendingUser = userRepository.GetSingleUser(newPost.Recipient);
            //newPost.Recipient = receivingUser.fullname;
            //newPost.Sender = receivingUser.fullname;

            using (var db = ConnectionHelper.CreateSqlConnection())
            {
                db.Query<Post>("INSERT INTO Posts (Sender, Recipient, Content) VALUES(@Sender, @Recipient, @Content);", newPost);
                
            }
        }
    }
}
