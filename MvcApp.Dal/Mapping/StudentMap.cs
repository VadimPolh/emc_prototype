using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            ToTable("Ученик");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_ученика").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Fio).HasColumnName("ФИО");
            Property(x => x.Pass).HasColumnName("Пароль");
            Property(x => x.Email).HasColumnName("Почта");
            Property(x => x.Age).HasColumnName("Возраст");
            HasOptional(x => x.Group).WithMany(x => x.Students).Map(x => x.MapKey("Id_группы")).WillCascadeOnDelete(true);
        }
    }
}
