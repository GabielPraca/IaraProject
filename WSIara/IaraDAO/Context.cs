using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IaraModels;

namespace IaraDAO
{
    class Context : DbContext
    {
        public DbSet<PersonalTask> PersonalTask { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}