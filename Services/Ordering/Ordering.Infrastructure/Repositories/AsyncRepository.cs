using Microsoft.EntityFrameworkCore;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        public readonly OrderContext context;
        public AsyncRepository(OrderContext _context)
        {
               context = _context; 
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> AddEntityAsync(T entity)
        {
             context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateEntityAsync(T entity)
        {
            context.Entry(entity).State= EntityState.Modified;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(T entity)
        {
             context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

       
    }
}
