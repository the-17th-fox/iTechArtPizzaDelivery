using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PD.Domain.Interfaces
{
    public interface IIngredientsRepository : IBaseRepository<Ingredient>
    {
        // Temporally blank
    }
}
