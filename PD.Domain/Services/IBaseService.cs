using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IBaseService<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(long id);
        public Task<T> AddAsync(T entity);
        public Task<T> DeleteAsync(long id);
    }
}
