using BasketService.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketService.ComponentTests.Configuration
{
    public class SqlLiteTestBase
    {
        protected static BasketDbContext GivenBasketContext()
        {
            return new BasketDbContext(new DbContextOptionsBuilder<BasketDbContext>()
                .UseSqlite(TestConstants.ConnectionString)
                .Options);
        }
    }
}
