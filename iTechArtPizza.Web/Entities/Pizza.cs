using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizza.Web.Entities
{
    public class Pizza
    {
     
        public long ID
        {
            get => _id;
            set
            {
                _id = value;
                _nextID++;
            }
        }
        private long _id;
        private static long _nextID;
        public string Title { get; set; }
        public string Description { get; set; }

        public Pizza(string title, string description)
        {
            ID = _nextID;
            Title = title;
            Description = description;
        }
    }
}
