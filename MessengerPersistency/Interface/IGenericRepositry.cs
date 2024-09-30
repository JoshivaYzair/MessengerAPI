using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessengerPersistency.Interface
{
    public interface IGenericRepositry<T> where T : class
    {
        Task InsertAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}
