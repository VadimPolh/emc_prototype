using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            ToTable("Преподаватель");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_преподавателя").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Fio).HasColumnName("ФИО");
            Property(x => x.Pass).HasColumnName("Пароль");
            Property(x => x.Exp).HasColumnName("Стаж");
            HasMany(x => x.Lessons).WithMany(x => x.Teachers).Map(m =>
            {
                m.ToTable("преподаватель_занятие");
                m.MapLeftKey("id_преподавателя");
                m.MapRightKey("id_занятия");
            });
        }
    }
}
