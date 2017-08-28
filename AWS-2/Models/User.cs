using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class User : IdentityUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int phone { get; set; }
        public DateTime JoinDate { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}