using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Controllers.api.Exceptions;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.api.Scaffolding
{
    public static class GenericExtensions
    {
        public static async Task<T> AssertFound<T>(this Task<T> func)
        {
            var entity = await func.ConfigureAwait(false);
            if (entity == null) throw new HttpNotFoundException();
            return entity;
        }

        public static T WithValidId<T>(this T entity, long id)
        {
            if (id <= 0)
                throw new HttpBadRequestException();
            return entity;
        }

        public static long WithValidId(this long id)
        {
            if (id <= 0)
                throw new HttpBadRequestException();
            return id;
        }

        public static T WithModelPresent<T>(this T entity)
        {
            if (entity == null)
                throw new HttpBadRequestException();
            return entity;
        }
        
        public static T WithValidModel<T>(this T entity, bool isValid)
        {
            if (!isValid)
                throw new HttpBadRequestException();
            return entity;
        }

        public static async Task<IHttpActionResult> Created<T>(this Task<T> entity, Uri requestUri, ApiController controller)
            => new CreatedNegotiatedContentResult<T>(requestUri, await entity, controller);

        public static async Task<IHttpActionResult> Ok<T>(this Task<T> entity, ApiController controller)
            => new OkNegotiatedContentResult<T>(await entity, controller);

        public static async Task<IHttpActionResult> NoContent(this Task task, ApiController controller)
        {
            await task;
            return new StatusCodeResult(HttpStatusCode.NoContent, controller.Request);
        }

        public static async Task<TEntity> CreateEntity<TEntity,TRequest>(this TRequest entity, IGenericService<TEntity,TRequest> service) 
            where TEntity:Entity
            where TRequest: EntityRequest
        {
            try
            {
                return await service.AddAsync(entity);
            }
            catch (ConcurrencyException)
            {
                throw new HttpConflictException();
            }
        }

        public static async Task<TEntity> UpdateEntity<TEntity, TRequest>(this TRequest entity, long id, IGenericService<TEntity, TRequest> service) 
            where TEntity : Entity
            where TRequest : EntityRequest
        {
            try
            {
                return await service.UpdateAsync(id, entity);
            }
            catch (ConcurrencyException)
            {
                throw new HttpConflictException();
            }
            catch (UnknownEntityException)
            {
                throw new HttpNotFoundException();
            }
        }

        public static async Task DeleteEntity<TEntity, TRequest>(this long id, IGenericService<TEntity, TRequest> service)
            where TEntity : Entity
            where TRequest : EntityRequest
        {
            try
            {
                await service.DeleteAsync(id);
            }
            catch (UnknownEntityException)
            {
                throw new HttpNotFoundException();
            }
        }


    }
}