﻿using hcode.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hcode.Repository.imp
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _entities.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public TEntity FindById(int id)
        {
            return _entities.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                TEntity entity = FindById(id);
                _entities.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TEntity Find(params Expression<Func<TEntity, bool>>[] filter)
        {
            IQueryable<TEntity> query = _entities;
            foreach (var filterProperty in filter)
            {
                query = query.Where(filterProperty);
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }
    }
}
