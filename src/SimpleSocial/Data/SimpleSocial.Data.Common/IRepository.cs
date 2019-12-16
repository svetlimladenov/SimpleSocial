﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSocial.Data.Common
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
