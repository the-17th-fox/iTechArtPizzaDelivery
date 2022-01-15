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
    [Authorize(Roles = RolesNames.USER)]
    [Route("api/order")]
    [ApiController]
    public class PublicOrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        public PublicOrdersController(IOrdersService service)
        {
            _ordersService = service;
        }

        [Route("active")]
        [HttpGet]
        public async Task<IActionResult> GetUsersActiveOrderAsync()
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.GetUsersActiveOrderAsync(userId));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync()
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.AddAsync(userId));
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.DeleteActiveOrderAsync(userId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPizzaAsync(long pizzaId, int numOfPizzasToAdd = 1)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.AddPizzaAsync(userId, pizzaId, numOfPizzasToAdd));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePizzaAsync(long pizzaId, int numOfPizzasToRemove = 1)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.RemovePizzaAsync(userId, pizzaId, numOfPizzasToRemove));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> UpdateDeliveryMethodAsync(int methodId)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.UpdateDeliveryMethodAsync(userId, methodId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> UpdateDescriptionAsync(string newDescription)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.UpdateDescriptionAsync(userId, newDescription));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> UpdatePromoCodeAsync(string promoCodeName)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.UpdatePromoCodeAsync(userId, promoCodeName));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> UpdateAdressAsync(string adress)
        {
            long userId = long.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _ordersService.UpdateAdressAsync(userId, adress));
        }
    }
}
