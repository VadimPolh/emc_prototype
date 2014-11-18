using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class LessonTypeMap : EntityTypeConfiguration<LessonType>
    {
        public LessonTypeMap()
        {
            ToTable("Вид_Занятия");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_вида_занятия").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Name).HasColumnName("Наименование");
            Property(x => x.Topic).HasColumnName("Тема");
        }
    }
}
