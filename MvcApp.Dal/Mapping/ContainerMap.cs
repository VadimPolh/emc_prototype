using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class ContainerMap : EntityTypeConfiguration<Container>
    {
        public ContainerMap()
        {
            ToTable("Ячейка");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_ячейки").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.PairNumber).HasColumnName("Номер_пары");
            Property(x => x.Room).HasColumnName("Аудитория");
            HasOptional(x => x.Lesson).WithMany(x => x.Containers).Map(x => x.MapKey("Id_занятия"));
            HasOptional(x => x.Group).WithMany(x => x.Containers).Map(x => x.MapKey("Id_группы"));
        }
    }
}
