using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Abstractions;
using DataAccessLayer.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjeContext _projeContext;

        public UnitOfWork(ProjeContext projeContext)
        {
            _projeContext = projeContext;
        }

        public async ValueTask DisposeAsync()
        {
            await _projeContext.DisposeAsync();
        }

        public int Save()
        {
            return _projeContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _projeContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(_projeContext);
        }
    }
}
