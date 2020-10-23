using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitApi.Data.Database;

namespace VisitApi.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly VisitContext VisitContext;

        public Repository(VisitContext visitContext)
        {
            VisitContext = visitContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return VisitContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await VisitContext.AddAsync(entity);
                await VisitContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                VisitContext.Update(entity);
                await VisitContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateRangeAsync)} entities must not be null");
            }

            try
            {
                VisitContext.UpdateRange(entities);
                await VisitContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be updated {ex.Message}");
            }
        }
    }
}