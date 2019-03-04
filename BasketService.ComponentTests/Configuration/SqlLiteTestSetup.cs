using System;
using Microsoft.EntityFrameworkCore;

namespace BasketService.ComponentTests.Configuration
{
    public class SqlLiteTestSetup : SqlLiteTestBase, IDisposable
    {
        public SqlLiteTestSetup()
        {
            DestroyDatabase();
            CreateDatabase();
        }

        private static void CreateDatabase()
        {
            using (var context = GivenBasketContext())
            {
                context.Database.Migrate();
            }
        }

        private static void DestroyDatabase()
        {
            using (var context = GivenBasketContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        public void Dispose()
        {
            DestroyDatabase();
        }
    }
}
