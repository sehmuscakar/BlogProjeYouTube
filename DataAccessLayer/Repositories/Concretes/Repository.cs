using CoreLayer.Entities;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concretes
{
    public class Repository<T> : IRepository<T>  where T : class,IBaseEntity, new()
    {
        private readonly ProjeContext _projeContext; //readonly :sadece oku 

        public Repository(ProjeContext projeContext)
        {
            _projeContext = projeContext;
        }
        //task=void
        private DbSet<T> Table { get => _projeContext.Set<T>(); } // buda generic olarak başka bir yöntem
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);   
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
           return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<List<T>> GetAllAsyc(Expression<Func<T,bool>> predicate=null,params Expression<Func<T, object>>[] includeProperties) //bool kullanma sebebimiz filtreleme yapmak için veri çekerken
        {
            IQueryable<T> query = Table;
            if (predicate !=null)
            {
                query = query.Where(predicate);

            }

            if (includeProperties.Any()) // any:herhangi , herhangi biri olduunda en az 
            {
                foreach (var item in includeProperties)
                {
                    query=query.Include(item);
                }
            }

            return await query.ToListAsync();


        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any()) // any:herhangi , herhangi biri olduunda en az 
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
        
            }
            return await query.SingleAsync();//bir tane değer döndüreceğimiz için single kuullanabiliriz tekli yani 

          }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }
         
        public async Task<T> UpdateAsync(T entity) // update işlemleri asekronik metotlar ile gerçekleşmez çüünkü update işlemi id ile veri tutması ile olduğu için data varlığını kesin kabul eder ,asekronik işlem aynı anda birden fazla işlem gerçekleştiği için update te olmaz 
        {
            await Task.Run(() => Table.Update(entity)); // entityframwork te updatetin asekronik olanı yok mesela bu yuzden büyle bir algoritma yazdık yapıyı bozmamamak için 
            return entity;
        
        }
    }
}
