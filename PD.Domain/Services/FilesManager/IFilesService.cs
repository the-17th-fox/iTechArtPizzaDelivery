using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IFilesService
    {
        public Task<FileViewModel> LoadFileAsync(string fileName);
        public Task<string> DeleteFileAsync(string fileName);
    }
}
