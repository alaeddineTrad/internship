using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class User
    {
        public long Id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public string mail { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}