using AutoMapper;
using MyTested.WebApi;
using WebApi.Controllers.api.Scaffolding;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
using Xunit;

namespace WebApi.Tests.Integration.Scaffolding
{
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
        :IClassFixture<AuthenticatedServerFixtureWithEntities<TEntity>>
        where TEntity: Entity, new()
        where TRequest: EntityRequest, new()
        where TController: GenericApiController<TEntity, TRequest>
    {
        private readonly AuthenticatedServerFixtureWithEntities<TEntity> _fixture;
        private IServerBuilder Server => _fixture.Server;
        private TEntity Entity1 => _fixture.Entity1;
        private TEntity Entity2 => _fixture.Entity2;
        private ApplicationDbContext CreateDbContext() => ApplicationDbContext.Create(_fixture.DbConnection);
        private readonly string _endpoint;

        protected IntegrationTestsBase(AuthenticatedServerFixtureWithEntities<TEntity> fixture)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TRequest, TEntity>(MemberList.Source).ReverseMap());

            _fixture = fixture;
            _endpoint = typeof(TController).Name.Replace("Controller", "");
        }

        private void CleanUp(TEntity actual)
        {
            if (actual == null) return;
            using (var dbContext = CreateDbContext())
            {
                var entity = dbContext.Set<TEntity>().Find(actual.Id);
                if (entity == null) return;
                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}