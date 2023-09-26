using CinemaTicketBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CinemaTicketBookingApi.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await context.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();

        }

        public async Task SaveChangesAsync()
        {
            var currentTime = DateTime.Now;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = currentTime;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = currentTime;
                    }
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
