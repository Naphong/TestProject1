using TestProject.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestProject.DataProcess
{
    public class TestProjectContext : DbContext
    {
        public TestProjectContext()
           : base("name=TestProjectContext")
        {
        }
        public DbSet<TransactionData> TransactionData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<TransactionData>();
            
        }
    }
}