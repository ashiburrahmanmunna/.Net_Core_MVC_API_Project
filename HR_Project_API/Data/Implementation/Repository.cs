using Microsoft.EntityFrameworkCore;

using HR_Project_API.Data.Interface;

using System;
using System.Linq.Expressions;

namespace HR_Project_API.Data.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext db;    
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().CountAsync(predicate);
            //throw new NotImplementedException();
        }

        public async Task Create(T model)
        {
            await db.Set<T>().AddAsync(model);
            await SaveChanges();
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                db.Set<T>().Remove(entity);
                await SaveChanges();
            }
        }



        public async Task<bool> Exist(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().AnyAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> Update(T model, string id)
        {
            if (model == null)
            {
                return null;
            }

            var oldData = await db.Set<T>().FindAsync(id);
            if (oldData != null)
            {
                db.Entry(oldData).CurrentValues.SetValues(model);
                await SaveChanges();
            }
            return oldData;
        }


        //public async Task<T> Update(T model, string Id)
        //{
        //    if (model == null)
        //    {
        //        return null;
        //    }
        //    T oldData = await db.Set<T>().FindAsync(Id);
        //    if (oldData != null)
        //    {
        //        db.Entry(oldData).CurrentValues.SetValues(model);
        //        await SaveChanges();
        //    }
        //    return model;
        //}

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return  db.Set<T>().Where(predicate);
        }
        protected async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
    }
}
