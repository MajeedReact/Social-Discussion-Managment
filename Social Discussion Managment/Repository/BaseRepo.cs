using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Discussion_Managment.Entities;

namespace Social_Discussion_Managment.Repository
{
    public class BaseRepo
    {
        private readonly string connectString;
        protected BaseRepo()
        {
            connectString = ConfigurationManager.ConnectionStrings["SDM"].ConnectionString;
        }

        protected SqlConnection GetConnection()
        {

            return new SqlConnection(connectString);
        }

        public void OpenConnection(SqlConnection con)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
        }



        public List<User> GetUsers(string query)
        {
            try
            {
                var con = GetConnection();
                //Open connection
                OpenConnection(con);
                var users = new List<User>();
                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();
                while (reader.Read()){
                   var user = new User();
                    user.Id = int.Parse(reader[0].ToString());
                    user.Username= reader[1].ToString();
                    DateTime dateTime = (DateTime)reader[3];
                    user.CreatedAt = dateTime.ToString("dd/MM/yyyy");
                    user.Role = reader[4].ToString();
                    
                    //add the user to the list
                    users.Add(user);
                }
                return users;
            }
            finally {

                CloseConnection();
            }

        }

        public User LogIn(string query, string username, string password)
        {
            try
            {
                var con = GetConnection();
                //Open connection
                OpenConnection(con);
                var cmd = new SqlCommand(query, con);

                cmd.Parameters.Add("@username", username);
                cmd.Parameters.Add("@password", password);

                User user = new User();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user.Id = int.Parse(reader[0].ToString()) ;
                    user.Username = reader[1].ToString();
                    user.CreatedAt = reader[3].ToString();
                    user.Role = reader[4].ToString();
                    return user;

                }
                return null;

            }
            finally
            {
                CloseConnection();
            }

        }

        public bool DeleteUser(string query, int id)
        {
            try
            {
                var con = GetConnection();
                //Open connection
                OpenConnection(con);
                var cmd = new SqlCommand(query, con);

                cmd.Parameters.Add("@id", id);

                var reader = cmd.ExecuteNonQuery();

                //if number of rows deleted more than 0 which is 1 true
                return reader > 0 ? true : false;

            }
            finally
            {
                CloseConnection();
            }

        } 

        public List<Comment> GetCommentsRelatedToPost(string query, int id)
        {
            try { 
            var con = GetConnection();
            OpenConnection(con);

            var cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@id", id);
            var reader = cmd.ExecuteReader();
            List<Comment> comments = new List<Comment>();
            while (reader.Read())
            {
                var comment = new Comment();
                comment.Id = int.Parse(reader[0].ToString());
                comment.desc = reader[1].ToString();
                comment.user_id = int.Parse(reader[2].ToString());

                comments.Add(comment);
            }
            return comments;
            }finally { CloseConnection(); }
        }
        public void DeletePost(string query, int id)
        {
            try
            {
                var con = GetConnection();
                //Open connection
                OpenConnection(con);
                var cmd = new SqlCommand(query, con);

                cmd.Parameters.Add("@id", id);

                var reader = cmd.ExecuteNonQuery();


            }
            finally
            {
                CloseConnection();
            }

        }

        public bool RegisterUser(string query, User user)
        {
            var con = GetConnection();
            OpenConnection(con);
            
            var cmd = new SqlCommand(query, con);
            //passing values to query
            cmd.Parameters.Add("@username", user.Username);
            cmd.Parameters.Add("@password", user.Password);
            cmd.Parameters.Add("@created_at", DateTime.Now);
            cmd.Parameters.Add("@role", user.Role);
            
            var result = cmd.ExecuteNonQuery();
            return result > 0 ? true : false;

        }
        public bool CreateAPost(string query, Post post)
        {
            try {  
            var con = GetConnection();
            OpenConnection(con);

            var cmd = new SqlCommand(query, con);
            //passing values to query
            cmd.Parameters.Add("@title", post.title);
            cmd.Parameters.Add("@desc", post.desc);
            cmd.Parameters.Add("@user_id", post.user_id);

            var result = cmd.ExecuteNonQuery();
            return result > 0 ? true : false;
            }
            finally { CloseConnection(); }
        }
        public void CreateComment(string query, Comment comment)
        {
            var con = GetConnection();
            OpenConnection(con);
            
            var cmd = new SqlCommand(query, con);
            //passing values to query
            cmd.Parameters.Add("@desc", comment.desc);
            cmd.Parameters.Add("@user_id", comment.user_id);
            cmd.Parameters.Add("@post_id", comment.post_id);
            
            cmd.ExecuteNonQuery();

        }  
        public void DeleteComment(string query, int id)
        {
            var con = GetConnection();
            OpenConnection(con);
            
            var cmd = new SqlCommand(query, con);
            //passing values to query
            cmd.Parameters.Add("@id", id);
            
            var result = cmd.ExecuteNonQuery();

        } 
        public bool UpdateUser(string query, User user)
        {
            var con = GetConnection();
            OpenConnection(con);
            
            var cmd = new SqlCommand(query, con);
            //passing values to query
            cmd.Parameters.Add("@username", user.Username);
            cmd.Parameters.Add("@password", user.Password);
            cmd.Parameters.Add("@id", user.Id);
            
            var result = cmd.ExecuteNonQuery();
            return result > 0 ? true : false;

        }

        public List<Comment> GetAllComments(string query)
        {
            try { 
            var con = GetConnection();
            OpenConnection(con);

            var cmd = new SqlCommand(query, con);

            var reader = cmd.ExecuteReader();
            var comments = new List<Comment>();

            while (reader.Read())
            {
                var comment = new Comment();
                comment.Id = int.Parse(reader[0].ToString());
                comment.desc = reader[1].ToString();
                comment.user_id = int.Parse(reader[2].ToString());
                comment.post_id = int.Parse(reader[3].ToString());

                comments.Add(comment);
            }
            return comments;
            }
            finally { CloseConnection(); }
     
            }
        public List<Post> GetAllPosts(string query)
        {
            try
            {
                var con = GetConnection();
                OpenConnection(con);

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();
                var posts = new List<Post>();

                while (reader.Read())
                {
                    var post = new Post();
                    post.Id = int.Parse(reader[0].ToString());
                    post.title = reader[1].ToString();
                    post.desc = reader[2].ToString();
                    post.user_id = int.Parse(reader[3].ToString());

                    posts.Add(post);
                }
                return posts;
            }
            finally { CloseConnection(); }

        }
        public void CloseConnection()
        {
            GetConnection().Close();
        }
    }
}
