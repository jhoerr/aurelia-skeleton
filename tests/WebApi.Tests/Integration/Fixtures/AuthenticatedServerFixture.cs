﻿using System;
using System.Data.Common;
using System.Web.Http;
using Xunit;

namespace WebApi.Tests.Integration.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DbConnection DbConnection { get; }

        public DatabaseFixture()
        {
            DbConnection = Effort.DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            // ... initialize data in the test database ...
        }


        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
