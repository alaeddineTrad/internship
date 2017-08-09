using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class DateTime2 : Convention
    {
        public DateTime2()
        {
            Properties<DateTime>().Configure(x => x.HasColumnType("DateTime2"));

        }
    }
}