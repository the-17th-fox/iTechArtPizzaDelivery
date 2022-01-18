using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;
        public FilesController(IFilesService filesService) => _filesService = filesService;

        [Route("[action]")]
        [HttpGet()]
        public async Task<IActionResult> LoadFileAsync(string fileName)
        {
            return Ok(_filesService.LoadFileAsync(fileName));
        }

        [Route("[action]")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteFileAsync(string fileName)
        {
            return Ok(_filesService.DeleteFileAsync(fileName));
        }
    }
}
