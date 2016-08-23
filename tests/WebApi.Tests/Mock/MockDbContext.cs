using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using WebApi.Models;

namespace WebApi.Tests.Mock
{
    public class MockDbContext<T>
        where T : Entity
    {
        public static ApplicationDbContext Create() => Create(new List<T>());

        public static ApplicationDbContext Create(List<T> entities)
        {
            var queryable = entities.AsQueryable();
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            // And then as you do:
            ((IQueryable<T>)mockSet).Provider.Returns(queryable.Provider);
            ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());

            mockSet.When(set => set.Add(Arg.Any<T>())).Do(info => entities.Add(info.Arg<T>()));
            mockSet.When(set => set.Remove(Arg.Any<T>())).Do(info => entities.Remove(info.Arg<T>()));

            var dbContext = Substitute.For<ApplicationDbContext>();
            dbContext.Set<T>().Returns(mockSet);
            return dbContext;
        }
    }
}