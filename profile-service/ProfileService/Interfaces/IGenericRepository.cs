using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Data;

namespace ProfileService.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}
