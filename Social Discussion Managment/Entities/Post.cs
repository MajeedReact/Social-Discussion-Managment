using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Discussion_Managment.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int user_id { get; set; }

    }
}
