using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using MyTested.WebApi;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Ploeh.AutoFixture;
using WebApi.Controllers.api.Exceptions;
using WebApi.Controllers.api.Scaffolding;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Unit.Controllers.api.Scaffolding
{
    [Collection("Database collection")]
    public abstract class GenericApiControllerTests<TEntity, TController, TRequest> 
        where TEntity: Entity, new()
        where TRequest: EntityRequest, new()
        where TController: GenericApiController<TEntity, TRequest>
    {
        private const int InvalidId = 9999;
        private readonly IGenericService<TEntity, TRequest> _service;
        private readonly TEntity _entity1;
        private readonly TEntity _entity2;
        private readonly TRequest _entity1Request;

        protected GenericApiControllerTests()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TRequest, TEntity>(MemberList.Source).ReverseMap());

            var fixture = new Fixture();
            _entity1 = fixture.Create<TEntity>();
            _entity2 = fixture.Create<TEntity>();
            _entity1Request = Mapper.Map<TRequest>(_entity1);

            _service = Substitute.For<IGenericService<TEntity, TRequest>>();

            _service.GetAllAsync().Returns(new List<TEntity>() {_entity1, _entity2});
            _service.GetAsync(_entity1.Id).Returns(_entity1);
            _service.AddAsync(_entity1Request).Returns(_entity1);
            _service.UpdateAsync(InvalidId, Arg.Any<TRequest>()).Throws<UnknownEntityException>();
            _service.UpdateAsync(_entity1.Id, _entity1Request).Returns(_entity1);
            _service.DeleteAsync(InvalidId).Throws<UnknownEntityException>();
        }

        /* /// GET ALL */

        [Fact]
        public void GetAllReturnsCollection()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Get())
                .ShouldReturn()
                .ResultOfType<IEnumerable<TEntity>>()
                .Passing(actual =>
                    actual.Contains(_entity1)
                    && actual.Contains(_entity2));
        }

        /* /// GET ONE */

        [Fact]
        public void GetByIdReturnsOne()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Get(_entity1.Id))
                .ShouldReturn()
                .ResultOfType<TEntity>()
                .Passing(actual => actual == _entity1);
        }

        [Fact]
        public void GetByIdReturnsNotFound()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Get(0))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpNotFoundException>();
        }

        /* /// POST */

        [Fact]
        public void PostReturnsCreatedStatusCode()
        {
            var entity = new Fixture().Create<TRequest>();

            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(entity))
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void PostReturnsEntity()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(_entity1Request))
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<TEntity>()
                .Passing(actual => actual == _entity1);
        }

        [Fact]
        public void PostPassesEntityToService()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(_entity1Request))
                .ShouldReturn()
                .Created();

            _service.Received().AddAsync(_entity1Request);
        }

        [Fact]
        public void PostValidatesEntityModel()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(new TRequest()))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

        [Fact]
        public void PostRequiresEntityModel()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(null))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

        [Fact]
        public void PostValidatesEntityUniqueness()
        {
            var entity = new Fixture().Create<TRequest>();
            _service.AddAsync(entity).Throws(new ConcurrencyException());

            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Post(entity))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpConflictException>();
        }

        /* /// PUT */

        [Fact]
        public void PutHasOkStatusCode()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(_entity1.Id, _entity1Request))
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PutReturnsEntity()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(_entity1.Id, _entity1Request))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<TEntity>()
                .Passing(actual => actual == _entity1);
        }

        [Fact]
        public void PutPassesEntityToService()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(_entity1.Id, _entity1Request));

            _service.Received().UpdateAsync(_entity1.Id, _entity1Request);
        }

        [Fact]
        public void PutValidatesId()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(0, _entity1Request))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

        [Fact]
        public void PutValidatesEntityModel()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(_entity1.Id, new TRequest()))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

        [Fact]
        public void PutRequiresEntityModel()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(_entity1.Id, null))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

        [Fact]
        public void PutHasNotFoundStatusCodeWhenPassedInvalidId()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Put(InvalidId, _entity1Request))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpNotFoundException>();
        }

        /* /// DELETE */

        [Fact]
        public void DeleteHasNoContentStatusCode()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Delete(_entity1.Id))
                .ShouldReturn()
                .StatusCode(HttpStatusCode.NoContent);
        }

        [Fact]
        public void DeleteHasNotFoundStatusCodeWhenPassedInvalidId()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Delete(InvalidId))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpNotFoundException>();
        }

        [Fact]
        public void DeleteValidatesId()
        {
            MyWebApi
                .Controller<TController>()
                .WithResolvedDependencyFor(_service)
                .CallingAsync(c => c.Delete(0))
                .ShouldThrow()
                .AggregateException(1)
                .ContainingInnerExceptionOfType<HttpBadRequestException>();
        }

    }
}