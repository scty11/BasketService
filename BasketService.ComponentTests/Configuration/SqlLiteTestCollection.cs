using Xunit;
namespace BasketService.ComponentTests.Configuration
{
    [CollectionDefinition("SQL server test collection")]
    public class SqlLiteTestCollection : ICollectionFixture<SqlLiteTestSetup>
    {
    }
}
