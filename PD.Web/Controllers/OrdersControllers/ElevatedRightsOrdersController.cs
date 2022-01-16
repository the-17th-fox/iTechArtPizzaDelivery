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
        public ElevatedRightsOrdersController(IOrdersService service) => _ordersService = service;

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PageSettingsViewModel pageSettings)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _ordersService.GetAllAsync(pageSettings));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _ordersService.GetByIdAsync(id));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long orderId)
        {
            return Ok(await _ordersService.DeleteAnyAsync(orderId));
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateIsActiveStatusAsync(long orderId, bool status)
        {
            return Ok(await _ordersService.UpdateIsActiveStatusAsync(orderId, status));
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrderStatusAsync(long orderId, int statusId)
        {
            return Ok(await _ordersService.UpdateOrderStatusAsync(orderId, statusId));
        }
    }
}
