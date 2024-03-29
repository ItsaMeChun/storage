﻿using System.Linq.Expressions;

namespace hcode.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);
        bool Delete(int id);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        bool Update(TEntity entity);
        TEntity Find(params Expression<Func<TEntity, bool>>[] filter);
        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
