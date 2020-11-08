using System;
using ClientApi.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace ClientApi.Data.Tests.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly ClientContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<ClientContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new ClientContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}
