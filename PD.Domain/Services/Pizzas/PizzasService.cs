﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Entities;
using PD.Domain.Interfaces;

namespace PD.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _repository;
        public PizzasService(IPizzasRepository repository) => _repository = repository;

        public async Task<List<Pizza>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Pizza> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<Pizza> AddAsync(string name, string description) 
            => await _repository.AddAsync(name, description);

        public async Task<Pizza> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId) 
            => await _repository.AddIngredientToPizzaAsync(ingredientId, pizzaId);
    }
}
