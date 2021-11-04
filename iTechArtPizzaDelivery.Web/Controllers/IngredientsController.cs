﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtIngredientDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IIngredientsService _ingredientsService;
        public IngredientsController(IIngredientsService ingredientsService) => _ingredientsService = ingredientsService;

        [ActionName(nameof(GetAllIngredientsAsync))]
        [HttpGet]
        public async Task<List<Ingredient>> GetAllIngredientsAsync() => await _ingredientsService.GetIngredientsAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Ingredient> GetIngredientAsync(int id) => await _ingredientsService.GetIngredientAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddIngredientAsync(string name)
        {
            try
            {
                Ingredient newIngredient = await _ingredientsService.AddIngredientAsync(name);

                return CreatedAtAction(nameof(GetAllIngredientsAsync), new { id = newIngredient.IngredientID }, newIngredient);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                "Error while adding Ingredient");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteIngredientAsync(int id)
        {
            try
            {
                Ingredient IngredientToRemove = await _ingredientsService.DeleteIngredientAsync(id);

                return CreatedAtAction(nameof(GetAllIngredientsAsync), new { id = IngredientToRemove.IngredientID }, IngredientToRemove);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Error while removing Ingredient");
            }
        }
    }
}
