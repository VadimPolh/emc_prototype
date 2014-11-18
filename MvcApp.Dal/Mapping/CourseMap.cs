using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            ToTable("Курс");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_курса").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.Number).HasColumnName("Номер_курса");
            HasMany(x => x.Subjects).WithMany(x => x.Courses).Map(m =>
            {
                m.ToTable("предмет_курс");
                m.MapLeftKey("Id_курса");
                m.MapRightKey("Id_предмета");
            });
            HasOptional(x => x.Set).WithMany(x => x.Courses).Map(x => x.MapKey("Id_направления")).WillCascadeOnDelete(true);
        }
    }
}
