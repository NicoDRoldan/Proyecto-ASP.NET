using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbventaContext _dbContext;
        public GenericRepository(DbventaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entity;
            }
            catch 
            {
                throw;
            }
        }

        public Task<TEntity> Crear(TEntity entidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(TEntity entidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(TEntity entidad)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            throw new NotImplementedException();
        }
    }
}
