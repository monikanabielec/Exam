﻿using ExamContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExamMailSenderAPI.Data
{
    public class Repository<T>: IDisposable where T : Entity
    {
        private readonly Context context;
        private readonly ILogger logger;
        private DbSet<T> dbSet = null;

        public Repository(Context context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
            dbSet = context.Set<T>();
        }
        public async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<List<T>> GetListAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<int> AddAsync(T item)
        {
            try
            {
                await dbSet.AddAsync(item);
                return item.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, System.Reflection.MethodBase.GetCurrentMethod().ToString());
                return -1;
            }
            
        }
        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                await Task.Run(() => dbSet.Update(item));
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, System.Reflection.MethodBase.GetCurrentMethod().ToString());
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetAsync(id);
            if (item != null)
            {
                try
                {
                    await Task.Run(() => dbSet.Remove(item));
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, System.Reflection.MethodBase.GetCurrentMethod().ToString());
                    return false;
                }                
            }
            return false;
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).AsQueryable();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
