using System;
using System.Collections.Generic;
using System.Text;
using Social_Discussion_Managment.Business;
using Social_Discussion_Managment.Entities;

namespace Social_Discussion_Managment
{
    public class Program
    {
        public static StringBuilder UsersTable(List<User> users)
        {
            var table = String.Format("|{0,-5}|{1,-10}|{2,-10}|{3,-10}|", "ID", "USER NAME", "CREATED AT", "ROLE");
            var formmatedTable = new StringBuilder();
            formmatedTable.AppendLine(table);
            foreach (var user in users)
            {
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-10}|{2,-10}|{3,-10}|", "-", "-", "-", "-"));
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-10}|{2,-10}|{3,-10}|", user.Id, user.Username, user.CreatedAt, user.Role));

            }
            return formmatedTable;
        }

        public static StringBuilder PostsTable(List<Post> posts)
        {
            var table = String.Format("|{0,-5}|{1,-30}|{2,-50}|{3,-10}|", "ID", "TITLE", "DESCRIPTION", "USER ID");
            var formmatedTable = new StringBuilder();
            formmatedTable.AppendLine(table);
            foreach (var post in posts)
            {
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-30}|{2,-50}|{3,-10}|", "-", "-", "-", "-"));
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-30}|{2,-50}|{3,-10}|", post.Id, post.title, post.desc, post.user_id));

            }
            return formmatedTable;
        }

        public static StringBuilder CommentsTable(List<Comment> comments)
        {
            var table = String.Format("|{0,-5}|{1,-50}|{2,-10}|{3,-10}|", "ID", "DESCRIPTION", "USER ID", "POST ID");
            var formmatedTable = new StringBuilder();
            formmatedTable.AppendLine(table);
            foreach (var comment in comments)
            {
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-50}|{2,-10}|{3,-10}|", "-", "-", "-", "-"));
                formmatedTable.AppendLine(String.Format("|{0,-5}|{1,-50}|{2,-10}|{3,-10}|", comment.Id, comment.desc, comment.user_id, comment.post_id));

            }
            return formmatedTable;
        }
        static void Main(string[] args)
        {
            CRUD instance = new CRUD();

            User LoggedInUser = null;
            while(LoggedInUser == null)
            {
                Console.WriteLine("Welcome to Social Discussion Managment");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                int input = int.Parse(Console.ReadLine());
                switch(input)
                {
                    case 1:
                        {
                            Console.WriteLine("Register");
                            var result = RegisterUser();
                            if (result)
                                Console.WriteLine("Successfully Registered Log in please");
                            else
                                    Console.WriteLine("Registeration Failed");
                            break;
                        }

                    case 2:
                        {
                            Console.Write("Enter your username: ");
                            string username = Console.ReadLine().Trim();
                            Console.Write("Enter your password: ");
                            string password = Console.ReadLine().Trim();
                            LoggedInUser = instance.LogIn(username, password);

                            if (LoggedInUser == null) Console.WriteLine("Invalid Credentials");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input entry");
                            break;
                        }
                }
              
            }
            while (true)
           {
                Console.WriteLine("Logged in as " + LoggedInUser.Role);
                DisplayMenu(LoggedInUser.Role);
                int choice = int.Parse(Console.ReadLine().Trim());
                
                if(choice == 1 && LoggedInUser.Role.Equals("admin")){
                    GetAllUsers();
                    HandleUserMenuChoice();

                }else if(choice == 2)
                {
                    //Get all posts
                    GetAllPosts(LoggedInUser.Role);
                    HandlePostMenuChoice(LoggedInUser.Id);
                }
                else if(choice == 3)
                {
                    GetComments(LoggedInUser.Role);
                    HandleCommentMenuChoice(LoggedInUser.Id);

                }
                else if(choice == 0)
                {
                    ExitSystem();
                }
            }

        }

        
        public static void DisplayMenu(string Role)
        {
            if (Role.Equals("admin"))
            Console.WriteLine("1. Get all users");

            Console.WriteLine("2. Get all posts");
            Console.WriteLine("3. Get all comments");
            Console.WriteLine("0. Exit");
        }

        public static void GetAllPosts(string Role)
        {

            var result = PostsTable(CRUD.Instance.GetAllPosts());
            Console.WriteLine(result);
            if (Role.Equals("admin") || Role.Equals("poster")) { 
                Console.WriteLine("1. To Create a post");
                Console.WriteLine("2. To Update a post");
                Console.WriteLine("3. To Delete a post");
            }
            Console.WriteLine("4. To Get all comments related to a post");
            Console.WriteLine("0. Back to main menu");
        }

        public static void GetAllUsers() {

            var result = UsersTable(CRUD.Instance.GetUsers());
            Console.WriteLine(result);
            Console.WriteLine("1. To Delete a user");
            Console.WriteLine("2. To Update a user");
            Console.WriteLine("0. Back to main menu");
        }

        public static bool UpdateUser()
        {
            User user = new User();
            Console.WriteLine("Enter user ID: ");
            user.Id = int.Parse(Console.ReadLine().Trim()); 
            Console.WriteLine("Enter the new username: ");
            user.Username = Console.ReadLine().Trim();  
            Console.WriteLine("Enter the new password: ");
            user.Password = Console.ReadLine().Trim();

            return CRUD.Instance.UpdateUser(user);

        }

        public static void HandleUserMenuChoice()
        {
           int choice = int.Parse(Console.ReadLine().Trim());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the user ID to be deleted: ");
                        choice = int.Parse(Console.ReadLine().Trim());
                        if (DeleteUser(choice)) Console.WriteLine("User Deleted Successfully");
                        break;
                    }
                case 2:
                    {
                        UpdateUser();
                        break;
                    }
            }
        } 
        public static void HandlePostMenuChoice(int id)
        {
           int choice = int.Parse(Console.ReadLine().Trim());
            switch (choice)
            {
                case 1:
                    {
                        //create
                        var result = CreatePost(id);
                        if (result) Console.WriteLine("Successfully created a post");
                        break;
                    }
                case 2:
                    {
                        //update post here
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Enter the post ID to be deleted: ");
                        choice = int.Parse(Console.ReadLine().Trim());
                        CRUD.Instance.DeletePost(choice);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Enter the post ID you would you like to see the comments for: ");
                        choice = int.Parse(Console.ReadLine());
                        var result = CommentsTable(CRUD.Instance.GetCommentsRelatedToPost(choice));
                        Console.WriteLine(result);
                        break;
                    }    
            }
        }

        public static void HandleCommentMenuChoice(int id)
        {
            int choice = int.Parse(Console.ReadLine().Trim());
            switch (choice)
            {
                case 1:
                    {
                        CreateComment(id);
                        break;
                    }
                case 2:
                    {
                        DeleteComment();
                        break;
                    }
                case 3:
                    {

                        break;
                    }    
            }
        }
        public static bool DeleteUser(int id)
        {
           return CRUD.Instance.DeleteUser(id);
        }

        public static bool CreatePost(int id)
        {
            Post post = new Post();
            post.user_id = id;
            Console.Write("Enter the title of the post: ");
            post.title = Console.ReadLine();
            Console.Write("Enter the description of the post: ");
            post.desc = Console.ReadLine();

            return CRUD.Instance.CreatePost(post);
        }

        public static void ExitSystem()
        {
            Console.WriteLine("Shutting down the system..");
            System.Environment.Exit(0);
        }

        public static void GetComments(string Role) { 
            var result =  CommentsTable(CRUD.Instance.GetComments());
            Console.WriteLine(result);
            if(Role.Equals("commenter") || Role.Equals("admin")){ 
            Console.WriteLine("1. To Create a comment");
            Console.WriteLine("2. To Delete a comment");
            Console.WriteLine("3. To Update a comment");
            }
            Console.WriteLine("0. Back to main menu");
        }

        public static void CreateComment(int user_id)
        {
            Comment comment = new Comment();
            comment.user_id = user_id;
            Console.WriteLine("Which post would you like to comment on?: ");
            comment.post_id = int.Parse(Console.ReadLine());
            Console.Write("Write your description: ");
            comment.desc = Console.ReadLine();

            CRUD.Instance.CreateComment(comment);
        }
        public static void DeleteComment()
        {
            Console.WriteLine("Enter the ID of the comment to be deleted: ");
            int choice = int.Parse(Console.ReadLine().Trim());
            CRUD.Instance.DeleteComment(choice);
        }

        public static bool RegisterUser()
        {
            User user = new User();
            Console.Write("Enter the username: ");
            user.Username = Console.ReadLine().Trim().ToLower();
            Console.Write("Enter the password: ");
            user.Password = Console.ReadLine().Trim();
            Console.Write("Choose your role: ");
            Console.WriteLine("1. Poster \t 2. Commenter (Choose the number)");
            var choice = int.Parse(Console.ReadLine());
            if(choice.Equals(1))
            {
                user.Role = "poster";
            }else if(choice.Equals(2)) { 
                user.Role = "commenter";
            }else
            {
                return false;
            }
    
           
            return CRUD.Instance.RegisterUser(user);
        }
    }
}
