using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Discussion_Managment.Constants;
using Social_Discussion_Managment.Entities;
using Social_Discussion_Managment.Repository;

namespace Social_Discussion_Managment.Business
{
    public class CRUD : BaseRepo
    {
        public static readonly CRUD Instance = new CRUD();

        public List<User> GetUsers() {

            var query = ConstantsQuery.SelectAllUsers;
            return GetUsers(query);
        }

        public User LogIn(string username, string password) {

            var query = ConstantsQuery.LogIn;
            return LogIn(query, username, password);
        }

        public bool DeleteUser(int id)
        {
            return DeleteUser(ConstantsQuery.DeleteUser, id);
        }

        public bool RegisterUser(User user)
        {
            return RegisterUser(ConstantsQuery.RegisterUser, user);
        }
        public bool UpdateUser(User user)
        {
            return UpdateUser(ConstantsQuery.UpdateUser, user);
        }

        public List<Post> GetAllPosts()
        {
            return GetAllPosts(ConstantsQuery.SelectAllPosts);
        }
        public void DeletePost(int id)
        {
            DeletePost(ConstantsQuery.DeletePost, id);
        }
        public bool CreatePost(Post post)
        {
            return CreateAPost(ConstantsQuery.CreatePost, post);
        }
        public List<Comment> GetCommentsRelatedToPost(int id)
        {
            return GetCommentsRelatedToPost(ConstantsQuery.SelectAllCommentsRelatedToPost, id);
        }

        public List<Comment> GetComments()
        {
            return GetAllComments(ConstantsQuery.SelectAllComments);
        }

        public void CreateComment(Comment comment)
        {
            CreateComment(ConstantsQuery.CreateComment, comment);
        } 
        public void DeleteComment(int id)
        {
            DeleteComment(ConstantsQuery.DeleteComment, id);
        }
    }
}
