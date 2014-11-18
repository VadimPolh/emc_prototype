using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class SpecialtyMap : EntityTypeConfiguration<Specialty>
    {
        public SpecialtyMap()
        {
            ToTable("Специальность");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_специальности").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Name).HasColumnName("Название");
            Property(x => x.Mentor).HasColumnName("Зав_отделением");
            HasOptional(x => x.Branch).WithMany(x => x.Specialties).Map(x => x.MapKey("Id_отделения")).WillCascadeOnDelete(true);
        }
    }
}
