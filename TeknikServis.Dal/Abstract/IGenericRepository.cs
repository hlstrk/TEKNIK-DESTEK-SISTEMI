using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        T Add(T entity);
        T Get(int id);


        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);

        bool Remove(int id);
        bool Remove(T entity);
        T Update(T entity);

        //select adi,soyadi from A 
        IQueryable<TResult> GetAllSelect<TResult>(Expression<Func<T, TResult>> select);

        //select adi,soyadi from where no>10
        IQueryable<TResult> GetAllSelect<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> select);


        bool Remove(Expression<Func<T, bool>> predicate);
        decimal Max(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> column);
        decimal Min(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal>> column);
        int? Max(Expression<Func<T, bool>> predicate, Expression<Func<T, Nullable<int>>> column);
        int? Min(Expression<Func<T, bool>> predicate, Expression<Func<T, Nullable<int>>> column);
        decimal Sum(Expression<Func<T, bool>> predicate, Expression<Func<T, Nullable<decimal>>> column);
        int Count(Expression<Func<T, bool>> predicate);













    }
}
