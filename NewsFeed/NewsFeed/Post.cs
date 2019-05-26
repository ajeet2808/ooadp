using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class Post
    {
        public Post(string content, string postedBy)
        {
            Content = content;
            PostedBy = postedBy;
            _createdOn = DateTime.UtcNow;
            _updatedOn = DateTime.UtcNow;
            Id = Post.GetNextPostId();
            _comments = new Dictionary<string, Post>();
        }

        public static int PostsCount = 0;
        private static string GetNextPostId()
        {
            return (++PostsCount).ToString("000");
        }
        public string Id { get; set; }
        public string Content { get; set; }
        private Dictionary<string, Post> _comments;
        public IReadOnlyDictionary<string, Post> Comments { get { return _comments; } }
        private int _upVotes;
        public int UpVotes { get { return _upVotes; } }
        private int _downVotes;
        public int DownVotes { get { return _downVotes; } }
        public int Score { get { return UpVotes - DownVotes; } }
        private DateTime _createdOn;
        public DateTime CreateOn { get { return _createdOn; } }
        private DateTime _updatedOn;
        public DateTime UpdatedOn { get { return _updatedOn; } }
        public string PostedBy { get; set; }
        public string DisplayTime
        {
            get
            {
                var duration = DateTime.UtcNow - CreateOn;
                if (duration.TotalSeconds < 60)
                {
                    return $"{(int)duration.TotalSeconds} s ago";
                }
                if (duration.TotalMinutes < 60)
                {
                    return $"{(int)duration.TotalMinutes} m ago";
                }
                return $"{(int)duration.TotalHours} hr ago";
            }
        }

        public void UpVote()
        {
            _upVotes++;
        }

        public void DownVote()
        {
            _downVotes++;
        }

        public void Comment(string commentContent, string postedBy)
        {
            var comment = new Post(commentContent, postedBy);
            _comments.Add(comment.Id, comment);
            _updatedOn = comment.CreateOn;
        }

        public string GetDisplayText(string indentation = "")
        {
            var sb = new StringBuilder($"{indentation}id: {Id}{Environment.NewLine}")
                        .Append($"{indentation}({UpVotes} upvote, {DownVotes} downvotes){Environment.NewLine}")
                        .Append($"{indentation}{PostedBy}{Environment.NewLine}")
                        .Append($"{indentation}{Content}{Environment.NewLine}")
                        .Append($"{indentation}{DisplayTime}");
            indentation = $"{indentation}\t";
            foreach (var comment in Comments.Values)
            {
                sb.Append($"{Environment.NewLine}{comment.GetDisplayText(indentation)}");
            }
            return sb.ToString();
        }
    }
}