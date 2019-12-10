using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace MyFace.DataAccess
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        void CreateUser(User newUser);

        string GetPassword(string username);

        User GetSingleUser(string username);
    }

    public class UserRepository: IUserRepository
    {

        public IEnumerable<User> GetAllUsers()
        {
            using (var db = ConnectionHelper.CreateSqlConnection())
            {
                //TODO Fetch user list from user table instead of from posts and senders.


                return db.Query<User>("Select username, fullname from user_accounts").ToList();
            }
        }

        public User GetSingleUser(string usernameToFind)
        {
            usernameToFind = usernameToFind.ToLower();
            using (var db = ConnectionHelper.CreateSqlConnection())
            {
                return db.Query<User>("Select username, password, fullname from user_accounts WHERE username = '" + usernameToFind + "'", new { usernameToFind }).SingleOrDefault();
            }
        }

        public void CreateUser(User newUser)
        {
            newUser.username.ToLower();
            using (var db = ConnectionHelper.CreateSqlConnection())
            {
                db.Query<User>("INSERT INTO user_accounts (username, password, fullname) VALUES(@username, @password, @fullname);", newUser);
            }
        }

        public string GetPassword(string usernametoFind)
        {
            string thepasswordtest;

            usernametoFind.ToLower();
            using (var db = ConnectionHelper.CreateSqlConnection()) {
                

                thepasswordtest = db.Query<string>("SELECT password FROM user_accounts WHERE username ='" + usernametoFind + "'", new { usernametoFind }).SingleOrDefault();

                return thepasswordtest;
                 

                //string password = db.Query< "Select password From user_accounts " + "WHERE username =" + username;
                //book = connection.Query<Book>("Select * From book " + "WHERE book_id =" + book_id, new { book_id }).SingleOrDefault();
            }
        }
    }


}
