using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsFeed
{
    public class NewsFeedManager
    {
        public NewsFeedManager()
        {
            Users = new Dictionary<string, User>();
            Posts = new Dictionary<string, Post>();
        }

        public void CreateUser(string username)
        {
            if (!Users.TryAdd(username, new User(username)))
            {
                throw new Exception("User already exists!");
            }
        }
        private User _currentUser;
        public void SetCurrentUser(string username)
        {
            if (Users.TryGetValue(username, out var user))
            {
                _currentUser = user;
            }
            else
            {
                throw new Exception("User does not exists!");
            }
        }
        public User CurrentUser { get { return _currentUser; } }
        public Dictionary<string, User> Users { get; set; }
        public Dictionary<string, Post> Posts { get; set; }

        public void Post(string content)
        {
            if (_currentUser == null)
            {
                throw new Exception("Please login first!");
            }

            var post = new Post(content, CurrentUser.Username);
            _currentUser.Posts.Add(post);
            Posts.Add(post.Id, post);
        }

        public void UpVotePost(string postId)
        {
            if (Posts.TryGetValue(postId, out var post))
            {
                post.UpVote();
            }
            else
            {
                throw new Exception("Could not find post!");
            }
        }
        public void DownVotePost(string postId)
        {
            if (Posts.TryGetValue(postId, out var post))
            {
                post.DownVote();
            }
            else
            {
                throw new Exception("Could not find post!");
            }
        }
        public void CommentOnPost(string postId, string comment)
        {
            if (Posts.TryGetValue(postId, out var post))
            {
                post.Comment(comment, CurrentUser.Username);
            }
            else
            {
                throw new Exception("Could not find post!");
            }
        }

        public IEnumerable<Post> GetNewsFeeds()
        {
            var posts = Posts.Values.ToList().OrderBy(x => x, new PostComparer(CurrentUser.Followers));
            return posts;
        }

        public void ShowNewsFeeds()
        {
            var posts = GetNewsFeeds();
            foreach (var post in posts)
            {
                Console.WriteLine(post.GetDisplayText());
            }
        }

        public void FollowUser(string username)
        {
            if(Users.TryGetValue(username, out var followedUser))
            {
                followedUser.AddFollower(_currentUser);
            }
        }
    }
}
