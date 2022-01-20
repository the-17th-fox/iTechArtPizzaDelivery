using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IFilesService
    {
        public FileViewModel LoadFileAsync(string fileName);
        public string DeleteFileAsync(string fileName);
    }
}
