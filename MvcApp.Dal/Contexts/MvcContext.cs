using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace MvcApp.Dal.Contexts
{
    public partial class MvcContext : DbContext
    {
        public MvcContext()
            : base("DefaultConnection")
        {
            // Package Manager Console -> Enable-Migrations on this prj
            //Enable-Migrations [-ContextTypeName <String>] [-EnableAutomaticMigrations] 
            //[-MigrationsDirectory <String>] [-ProjectName <String>] [-StartUpProjectName <String>] 
            //[-ContextProjectName <String>] [-ConnectionStringName <String>] [-Force] 
            //[-ContextAssemblyName <String>] [-AppDomainBaseDirectory <String>] [<CommonParameters>]
 
            //Enable-Migrations [-ContextTypeName <String>] [-EnableAutomaticMigrations] 
            //[-MigrationsDirectory <String>] [-ProjectName <String>] [-StartUpProjectName <String>]
            //[-ContextProjectName <String>] -ConnectionString <String> 
            //-ConnectionProviderName <String> [-Force] [-ContextAssemblyName <String>] 
            //[-AppDomainBaseDirectory <String>] [<CommonParameters>]
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new AgeMap());
            //modelBuilder.Configurations.Add(new LeagueMap());
            //base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                          .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                          .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                                                         type.BaseType.GetGenericTypeDefinition() ==
                                                         typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
