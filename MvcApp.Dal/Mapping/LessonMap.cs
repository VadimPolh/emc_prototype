using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class LessonMap : EntityTypeConfiguration<Lesson>
    {
        public LessonMap()
        {
            ToTable("Занятие");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_занятия").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Name).HasColumnName("Наименование");
            HasOptional(x => x.Subject).WithMany(x => x.Lessons).Map(x => x.MapKey("Id_предмета")).WillCascadeOnDelete(true);
            HasOptional(x => x.LessonType).WithMany(x => x.Lessons).Map(x => x.MapKey("Id_вида_занятия")).WillCascadeOnDelete(true);
        }
    }
}
