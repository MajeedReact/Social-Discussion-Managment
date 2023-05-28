using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Discussion_Managment.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string desc { get; set; }
        public int user_id { get; set; }
        public int post_id { get; set; }
    }
}
