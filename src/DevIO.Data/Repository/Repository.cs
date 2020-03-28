using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AmcDbContext _amcDbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(AmcDbContext amcDbContext)
        {
            _amcDbContext = amcDbContext;
            dbSet = amcDbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            //AsNoTracking é mais performático
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await dbSet.ToListAsync();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid Id)
        {
            dbSet.Remove(new TEntity { Id = Id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _amcDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _amcDbContext?.Dispose();
        }

    }
}
