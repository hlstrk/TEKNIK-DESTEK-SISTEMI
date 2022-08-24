using TeknikServis.Dal.Abstract;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Bll
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> genericRepository;

        public GenericManager(IGenericRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public T Add(T entity)
        {

            return genericRepository.Add(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                genericRepository.Dispose();
            }
        }
        public T Get(int id)
        {
            return genericRepository.Get(id);
        }

        public List<T> GetAll()
        {
            return genericRepository.GetAll();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return genericRepository.GetAll(predicate);
        }

        public bool Remove(int id)
        {
            return genericRepository.Remove(id);
        }

        public bool Remove(T entity)
        {
            return genericRepository.Remove(entity);
        }

        public T Update(T entity)
        {
            return genericRepository.Update(entity);
        }

        public IQueryable<TResult> GetAllSelect<TResult>(Expression<Func<T, TResult>> select)
        {
            return genericRepository.GetAllSelect(select);
        }

        public IQueryable<TResult> GetAllSelect<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> select)
        {
            return genericRepository.GetAllSelect(predicate, select);
        }

        public bool Remove(Expression<Func<T, bool>> predicate)
        {
            return genericRepository.Remove(predicate);
        }

        public decimal Max(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> column)
        {
            return genericRepository.Max(predicate, column);
        }

        public decimal Min(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> column)
        {
            return genericRepository.Min(predicate, column);
        }

        public int? Max(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> column)
        {
            return genericRepository.Max(predicate, column);
        }

        public int? Min(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> column)
        {
            return genericRepository.Min(predicate, column);
        }

        public decimal Sum(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal?>> column)
        {
            return genericRepository.Sum(predicate, column);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return genericRepository.Count(predicate);
        }
    }
}
