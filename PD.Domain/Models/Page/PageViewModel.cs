using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class PageViewModel<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}
