using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.Pocos;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _context;
        private readonly DbSet<T> _dbSet;

        // Default constructor to handle test cases where no context is provided
        public EFGenericRepository()
        {
            _context = new CareerCloudContext(); // Create a default context
            _dbSet = _context.Set<T>();
        }

        // Overloaded constructor to use a provided context (useful for DI)
        public EFGenericRepository(CareerCloudContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.ToList();
        }

        public IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.Where(where).ToList();
        }

        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query.SingleOrDefault(where);
        }

        public void Add(params T[] items)
        {
            _dbSet.AddRange(items);
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            _dbSet.UpdateRange(items);
            _context.SaveChanges();
        }

        public void Remove(params T[] items)
        {
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            var parameterString = string.Join(", ", parameters.Select(p => $"@{p.Item1} = {p.Item2}"));
            var query = $"EXEC {name} {parameterString}";
            _context.Database.ExecuteSqlRaw(query);
        }
    }
}
