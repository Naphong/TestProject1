using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace TestProject.DataProcess
{
    public class TestProjectConfiguration : DbConfiguration
    {
        public TestProjectConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}