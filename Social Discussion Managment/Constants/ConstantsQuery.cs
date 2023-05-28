using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Discussion_Managment.Constants
{
    public class ConstantsQuery
    {
        //User queries
        public const string SelectAllUsers = "SELECT * FROM USERS";
        public const string LogIn = "SELECT * FROM USERS WHERE Username = @username AND Password = @password";
        public const string DeleteUser = "DELETE FROM USERS WHERE Id = @id";
        public const string RegisterUser = "INSERT INTO USERS (username, password, created_at, role) VALUES (@username, @password, @created_at, @role)";
        public const string UpdateUser = "UPDATE USERS SET username = @username, password = @password WHERE id = @id";

        //Post queries
        public const string SelectAllPosts = "SELECT * FROM POSTS";
        public const string CreatePost = "INSERT INTO POSTS (title, description, user_id) VALUES (@title, @desc, @user_id)";
        public const string DeletePost = "DELETE FROM POSTS WHERE id = @id";
        public const string SelectAllCommentsRelatedToPost = "SELECT * FROM COMMENTS WHERE post_id = @id";

        //Comment queries
        public const string SelectAllComments = "SELECT * FROM COMMENTS";
        public const string CreateComment = "INSERT INTO COMMENTS (description, user_id, post_id) VALUES (@desc, @user_id, @post_id)";
        public const string DeleteComment = "DELETE FROM COMMENTS WHERE id = @Id";

    }
}
