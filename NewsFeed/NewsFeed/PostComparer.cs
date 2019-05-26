using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeed
{
    public class PostComparer : IComparer<Post>
    {
        private IReadOnlyDictionary<string, User> _followers;
        public PostComparer(IReadOnlyDictionary<string, User> followers)
        {
            _followers = followers;
        }

        public int Compare(Post x, Post y)
        {
            if (x == null && y != null) return -1;
            if (x != null && y == null) return 1;
            if (x == null || y == null) return 0;
            var isXByFollowedUser = _followers.ContainsKey(x.PostedBy);
            var isYByFollowedUser = _followers.ContainsKey(y.PostedBy);
            int comparedValue = isXByFollowedUser && !isYByFollowedUser ? 1 : (!isXByFollowedUser && isYByFollowedUser ? -1 : 0);
            if (comparedValue != 0) return comparedValue;
            comparedValue = x.Score - y.Score;
            if (comparedValue != 0) return comparedValue;
            comparedValue = x.Comments.Count - y.Comments.Count;
            if (comparedValue != 0) return comparedValue;
            comparedValue = x.UpdatedOn > y.UpdatedOn ? 1 : (x.UpdatedOn < y.UpdatedOn ? -1 : 0);
            return comparedValue;
        }
    }
}
