using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public class Interaction
    {
        [Key]
        [Column(Order=1)]
        public long TravlerId { get; set; }
        [Key]
        [Column(Order =2)]
        public long RecieverId { get; set; }
        public DateTime date { get; set; }

        public virtual Travler Travler { get; set; }
        public virtual Reciever Reciever { get; set; }


    }
}