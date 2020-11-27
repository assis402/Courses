using Courses.AccessData.Interfaces;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await PickById(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var entity = await PickById(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> PickAll()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PickById(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PickById(string id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
