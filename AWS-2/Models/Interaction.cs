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
        public long InteractionId { get; set; }
    
        public DateTime date { get; set; }

        public virtual Travler Travler { get; set; }
        public virtual Reciever Reciever { get; set; }


    }
}