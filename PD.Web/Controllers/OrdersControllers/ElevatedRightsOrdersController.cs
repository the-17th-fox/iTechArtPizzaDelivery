using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Models;
using PD.Domain.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PD.Web.Controllers.OrdersControllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    [Route("api/orders")]
    [ApiController]
    public class ElevatedRightsOrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        public ElevatedRightsOrdersController(IOrdersService service, IMapper mapper)
        {
            _ordersService = service;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        { 
            return Ok(await _ordersService.GetAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _ordersService.GetByIdAsync(id));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _ordersService.DeleteAsync(id));
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> ChangeIsPaidStatusAsync(long orderId, bool isPaid)
        {
            return Ok(await _ordersService.ChangeIsPaidStatusAsync(orderId, isPaid));
        }
    }
}
