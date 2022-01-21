using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class FileViewModel
    {
        public FileStream FileStream { get; set; }
        public string Extension { get; set; }
    }
}
