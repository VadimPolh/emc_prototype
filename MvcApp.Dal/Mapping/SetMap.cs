using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class SetMap : EntityTypeConfiguration<Set>
    {
        public SetMap()
        {
            ToTable("Направление");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_направления").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Name).HasColumnName("Наименование");
            HasOptional(x => x.Specialty).WithMany(x => x.Sets).Map(x => x.MapKey("Id_специальности")).WillCascadeOnDelete(true);
        }
    }
}
