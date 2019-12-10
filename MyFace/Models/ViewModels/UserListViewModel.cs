using System.Collections.Generic;
using MyFace.DataAccess;

namespace MyFace.Models.ViewModels
{
    public class UserListViewModel
    {
        public string Username { get; }
        public IEnumerable<User> ListOfUsers { get; }

        public UserListViewModel(string username, IEnumerable<User> listOfUsers)
        {

            Username = username;
            ListOfUsers = listOfUsers;
        }
    }
}