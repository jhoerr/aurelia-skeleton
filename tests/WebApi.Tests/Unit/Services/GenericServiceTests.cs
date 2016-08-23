using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Ploeh.AutoFixture;
using Shouldly;
using WebApi.Controllers.api.Exceptions;
using WebApi.Models;
using WebApi.Services;
using WebApi.Tests.Mock;
using Xunit;

namespace WebApi.Tests.Unit.Services
{
    public class WhenGetting
    {
        private readonly Customer _customer1;
        private readonly Customer _customer2;
        private readonly GenericService<Customer, CustomerRequest> _service;

        public WhenGetting()
        {

            var fixture = new Fixture();
            _customer1 = fixture.Create<Customer>();
            _customer2 = fixture.Create<Customer>();

            var data = new List<Customer>() {_customer1, _customer2};

            _service = new GenericService<Customer, CustomerRequest>(MockDbContext<Customer>.Create(data), MockIdentity.Create());
        }

        [Fact]
        public async Task GetAllFetchesTheCorrectNumberOfEntities()
        {
           var actual = await _service.GetAllAsync();

            actual.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetAllFetchesTheCorrectEntities()
        {
            var actual = await _service.GetAllAsync();

            actual.ShouldContain(_customer1);
            actual.ShouldContain(_customer2);
        }

        [Fact]
        public async Task GetFetchesASingleEntityById()
        {
            var actual = await _service.GetAsync(_customer1.Id);

            actual.ShouldBeSameAs(_customer1);
        }

        [Fact]
        public async Task GetDoesNotFindNonExistentEntity()
        {
            var actual = await _service.GetAsync(0);

            actual.ShouldBeNull();
        }
    }

    public class WhenAdding
    {
        private readonly Fixture _fixture = new Fixture();

        public WhenAdding()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CustomerRequest, Customer>(MemberList.Source).ReverseMap());
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public async Task ItAddsToTheSet()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request = _fixture.Create<CustomerRequest>();

            await service.AddAsync(request);

            dbContext.Set<Customer>().Single().Name.ShouldBeSameAs(request.Name);
        }

        [Fact]
        public void ItRejectsNullEntity()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.AddAsync(null).Wait());
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public async Task ItAddsDistinctEntities()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request1 = _fixture.Create<CustomerRequest>();
            var request2 = _fixture.Create<CustomerRequest>();

            await service.AddAsync(request1);
            await service.AddAsync(request2);

            var actual = dbContext.Set<Customer>().OrderBy(c => c.Id).ToList();
            actual.First().Name.ShouldBeSameAs(request1.Name);
            actual.Last().Name.ShouldBeSameAs(request2.Name);
        }

        [Fact]
        public async Task ItSavesChanges()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request1 = _fixture.Create<CustomerRequest>();

            await service.AddAsync(request1);

            dbContext.Received().SaveChangesAsync();
        }

        [Fact]
        public async Task ItSetsTheCreatedFields()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request1 = _fixture.Create<CustomerRequest>();

            var actual = await service.AddAsync(request1);

            actual.CreatedBy.ShouldBe(MockIdentity.Username);
            actual.CreatedOn.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow);
        }

        [Fact]
        public async Task ItSetsTheUpdatedFields()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request1 = _fixture.Create<CustomerRequest>();

            var actual = await service.AddAsync(request1);

            actual.ModifiedBy.ShouldBe(MockIdentity.Username);
            actual.ModifiedOn.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow);
        }

        [Fact]
        public async Task ItSetsTheCreatedAndModifiedFieldsToTheSameValues()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            var request1 = _fixture.Create<CustomerRequest>();

            var actual = await service.AddAsync(request1);

            actual.ModifiedBy.ShouldBe(actual.CreatedBy);
            actual.ModifiedOn.ShouldBe(actual.CreatedOn);
        }
    }

    public class WhenUpdating
    {
        private readonly Fixture _fixture = new Fixture();

        public WhenUpdating()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CustomerRequest, Customer>(MemberList.Source).ReverseMap());
            Mapper.Configuration.AssertConfigurationIsValid();

        }

        [Fact]
        public void ItRejectsInvalidId()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.UpdateAsync(0, new CustomerRequest()).Wait());
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void ItRejectsNullEntity()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.UpdateAsync(1, null).Wait());
            Assert.IsType<ArgumentNullException>(exception.InnerException);
        }

        [Fact]
        public void ItRejectsUnknownEntity()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.UpdateAsync(99999, new CustomerRequest()).Wait());
            Assert.IsType<UnknownEntityException>(exception.InnerException);
        }

        [Fact]
        public async Task ItUpdatesTheEntityFields()
        {
            var entity = _fixture.Create<Customer>();
            var request = Mapper.Map<CustomerRequest>(entity);

            request.Name = "new name";
            request.Address1 = "new address1";

            var dbContext = MockDbContext<Customer>.Create(new List<Customer>() {entity});
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());
            
            await service.UpdateAsync(entity.Id, request);

            entity.Name.ShouldBe(request.Name);
            entity.Address1.ShouldBe(request.Address1);
        }

        [Fact]
        public async Task ItSavesChanges()
        {
            var entity = _fixture.Create<Customer>();
            var request = Mapper.Map<CustomerRequest>(entity);
            request.Name = "new name";

            var dbContext = MockDbContext<Customer>.Create(new List<Customer>() { entity });
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            await service.UpdateAsync(entity.Id, request);

            dbContext.Received().SaveChangesAsync();
        }

        [Fact]
        public async Task ItDoesNotUpdateTheCreatedFields()
        {
            var expectedCreatedOn = new DateTime(2016,1,1);
            var expectedCreatedBy = "someuser";

            var entity = _fixture.Create<Customer>();
            entity.CreatedOn = expectedCreatedOn;
            entity.CreatedBy = expectedCreatedBy;

            var dbContext = MockDbContext<Customer>.Create(new List<Customer>() { entity });
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var request = Mapper.Map<CustomerRequest>(entity);
            await service.UpdateAsync(entity.Id, request);

            entity.CreatedBy.ShouldBe(expectedCreatedBy);
            entity.CreatedOn.ShouldBe(expectedCreatedOn);
        }

        [Fact]
        public async Task ItUpdatesTheModifiedFields()
        {
            var expectedCreatedOn = new DateTime(2016, 1, 1);
            var expectedCreatedBy = "someuser";

            var entity = _fixture.Create<Customer>();
            entity.CreatedOn = entity.ModifiedOn = expectedCreatedOn;
            entity.CreatedBy = entity.ModifiedBy = expectedCreatedBy;

            var dbContext = MockDbContext<Customer>.Create(new List<Customer>() { entity });
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var request = Mapper.Map<CustomerRequest>(entity);
            await service.UpdateAsync(entity.Id, request);

            entity.ModifiedBy.ShouldBe(MockIdentity.Username);
            entity.ModifiedOn.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow);
        }
    }

    public class WhenDeleting
    {
        private readonly Fixture _fixture = new Fixture();

        public WhenDeleting()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CustomerRequest, Customer>(MemberList.Source).ReverseMap());
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ItRejectsInvalidId()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.DeleteAsync(0).Wait());
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void ItRejectsUnknownEntity()
        {
            var dbContext = MockDbContext<Customer>.Create();
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            var exception = Assert.Throws<AggregateException>(() => service.DeleteAsync(99999).Wait());
            Assert.IsType<UnknownEntityException>(exception.InnerException);
        }

        [Fact]
        public async Task ItDeletesTheEntity()
        {
            var entity = _fixture.Create<Customer>();
            var entities = new List<Customer>() { entity };
            var dbContext = MockDbContext<Customer>.Create(entities);
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            await service.DeleteAsync(entity.Id);

            entities.ShouldBeEmpty();
        }

        [Fact]
        public async Task ItSavesChanges()
        {
            var entity = _fixture.Create<Customer>();

            var dbContext = MockDbContext<Customer>.Create(new List<Customer>() { entity });
            var service = new GenericService<Customer, CustomerRequest>(dbContext, MockIdentity.Create());

            await service.DeleteAsync(entity.Id);

            dbContext.Received().SaveChangesAsync();
        }
    }
}