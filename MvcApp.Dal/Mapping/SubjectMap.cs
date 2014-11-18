using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class SubjectMap : EntityTypeConfiguration<Subject>
    {
        public SubjectMap()
        {
            ToTable("Предмет");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_предмета").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Book).HasColumnName("Учебник");
            Property(x => x.Plan).HasColumnName("План");
            Property(x => x.Okr).HasColumnName("ОКР");
            Property(x => x.Test).HasColumnName("Тест");
            Property(x => x.HoursNumber).HasColumnName("Количество_часов");
        }
    }
}
