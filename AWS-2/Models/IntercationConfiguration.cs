using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class IntercationConfiguration : EntityTypeConfiguration<Interaction>
    {
        public IntercationConfiguration()
        {
            Map<Comment>(map => map.ToTable("Comment"));
            Map<Rate>(map => map.ToTable("Rate"));


        }
    }
}