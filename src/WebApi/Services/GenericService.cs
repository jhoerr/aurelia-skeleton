using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using NMemory.Exceptions;
using WebApi.Controllers.api.Exceptions;
using WebApi.Models;

namespace WebApi.Services
{
    public class GenericService<TEntity, TRequest> : IGenericService<TEntity, TRequest> 
        where TEntity : Entity
        where TRequest : EntityRequest
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _set;
        private readonly string _username;

        public GenericService(ApplicationDbContext context, IIdentity identity)
        {
            _username = identity.Name;
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>>  GetAllAsync() => await Task.Run(() => _set.ToList());

        public async Task<TEntity> GetAsync(long id) => await Task.Run(() => _set.SingleOrDefault(e => e.Id == id));

        public async Task<TEntity> AddAsync(TRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = Mapper.Map<TEntity>(request);

            entity.CreatedBy = entity.ModifiedBy = _username;
            entity.CreatedOn = entity.ModifiedOn = Now();

            _set.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(long id, TRequest request)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = await Task.Run(()=> _set.SingleOrDefault(e=>e.Id == id));
            if (entity == null) throw new UnknownEntityException();

            Mapper.Map(request, entity);
            entity.ModifiedBy = _username;
            entity.ModifiedOn = Now();

            await SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var entity = await Task.Run(() => _set.SingleOrDefault(e => e.Id == id));
            if (entity == null) throw new UnknownEntityException();

            _set.Remove(entity);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static DateTime Now()
        {
            var now = DateTime.UtcNow;
            return now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond));
        }

        public virtual void HandleException(Exception exception)
        {
            DbUpdateConcurrencyException concurrencyEx = exception as DbUpdateConcurrencyException;
            if (concurrencyEx != null)
            {
                // A custom exception of yours for concurrency issues
                throw new ConcurrencyException();
            }

            DbUpdateException dbUpdateEx = exception as DbUpdateException;
            if (dbUpdateEx != null)
            {
                if (dbUpdateEx != null
                        && dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    SqlException sqlException = dbUpdateEx.InnerException.InnerException as SqlException;
                    if (sqlException != null)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                            case 547:   // Constraint check violation
                            case 2601:  // Duplicated key row error
                                        // Constraint violation exception
                                throw new ConcurrencyException();   // A custom exception of yours for concurrency issues

                            default:
                                // A custom exception of yours for other DB issues
                                throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                        }
                    }

                    if (dbUpdateEx.InnerException.InnerException.InnerException != null)
                    {
                        //NMemory exceptions
                        MultipleUniqueKeyFoundException ex = dbUpdateEx.InnerException.InnerException.InnerException as MultipleUniqueKeyFoundException;
                        if (ex != null) throw new ConcurrencyException();
                    }


                    throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                }
            }

            // If we're here then no exception has been thrown
            // So add another piece of code below for other exceptions not yet handled...
        }
    }
}