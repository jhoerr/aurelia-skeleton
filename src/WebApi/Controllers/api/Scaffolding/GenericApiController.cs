using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.api.Scaffolding
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GenericApiController<TEntity, TRequest> : ApiController
        where TEntity: Entity
        where TRequest: EntityRequest
    {
        private readonly IGenericService<TEntity, TRequest> _service;

        public GenericApiController(IGenericService<TEntity, TRequest> service)
        {
            _service = service;
        }

        [Route("")]
        public async Task<IEnumerable<TEntity>> Get() 
            => await _service.GetAllAsync();

        [Route("{id:long}")]
        public async Task<TEntity> Get(long id) 
            => await _service.GetAsync(id).AssertFound();

        [Route("")]
        public async Task<IHttpActionResult> Post(TRequest entity)
            => await entity
                .WithModelPresent()
                .WithValidModel(ModelState.IsValid)
                .CreateEntity(_service)
                .Created(Request.RequestUri, this);

        [Route("{id:long}")]
        public async Task<IHttpActionResult> Put(long id, TRequest entity)
            => await entity
                .WithValidId(id)
                .WithModelPresent()
                .WithValidModel(ModelState.IsValid)
                .UpdateEntity(id, _service)
                .Ok(this);

        [Route("{id:long}")]
        public async Task<IHttpActionResult> Delete(long id)
            => await id
                .WithValidId()
                .DeleteEntity(_service)
                .NoContent(this);
    }
}