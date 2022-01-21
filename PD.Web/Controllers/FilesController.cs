using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Constants;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;
        public FilesController(IFilesService filesService) => _filesService = filesService;

        [Authorize(Roles = RolesNames.USER)]
        [Route("{fileName}")]
        [HttpGet()]
        public IActionResult LoadFileAsync(string fileName)
        {
            var fileModel = _filesService.LoadFileAsync(fileName);

            return File(fileModel.FileStream, $"image/{fileModel.Extension}");
        }

        [Authorize(Roles = RolesNames.ADMIN)]
        [Route("[action]/{fileName}")]
        [HttpDelete()]
        public IActionResult DeleteFileAsync(string fileName)
        {
            return Ok(_filesService.DeleteFileAsync(fileName));
        }
    }
}
