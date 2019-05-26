using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class User
    {

        public User(string username)
        {
            Username = username;
            Posts = new List<Post>();
            Followers = new Dictionary<string, User>();
        }
        public string Username { get; set; }
        public List<Post> Posts { get; set; }
        public Dictionary<string, User> Followers { get; set; }

        public void AddFollower(User follower)
        {
            Followers.TryAdd(follower.Username, follower);
        }
    }
}
