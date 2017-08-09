using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS_2.Models
{
    public enum Size
    {
        tiny,medium,large
    }
    public enum Category
    {
        clothes,technologie,Accessory
    }
    public class Item
    {
        public long Id { get; set; }
        public Size size { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public virtual User User { get; set; }
       
    }
}