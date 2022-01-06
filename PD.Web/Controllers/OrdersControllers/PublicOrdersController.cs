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
        public PublicOrdersController(IOrdersService service, IMapper mapper) => _ordersService = service;

        [Route("current")]
        [HttpGet]
        public async Task<IActionResult> GetActiveOrder()
        {
            //var r = User.Claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _ordersService.GetActiveAsync(long.Parse(userId)));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _ordersService.AddAsync(model));
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _ordersService.DeleteActiveAsync(long.Parse(userId)));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPizzaAsync(long userId, long pizzaId)
        {
            return Ok(await _ordersService.AddPizzaAsync(userId, pizzaId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePizzaAsync(long userId, long pizzaId)
        {
            return Ok(await _ordersService.RemovePizzaAsync(userId, pizzaId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPromoCodeAsync(long userId, long promoCodeId)
        {
            return Ok(await _ordersService.AddPromoCodeAsync(userId, promoCodeId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePromoCodeAsync(long orderId)
        {
            return Ok(await _ordersService.RemovePromoCodeAsync(orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddAdressAsync(long userId, string adress)
        {
            return Ok(await _ordersService.AddAdressAsync(userId, adress));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemoveAdressAsync(long userId)
        {
            return Ok(await _ordersService.RemoveAdressAsync(userId));
        }
    }
}
