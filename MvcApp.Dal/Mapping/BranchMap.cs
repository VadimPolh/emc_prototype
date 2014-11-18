using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            ToTable("Отделение");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_отделения").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Name).HasColumnName("Наименование");
        }
    }
}
