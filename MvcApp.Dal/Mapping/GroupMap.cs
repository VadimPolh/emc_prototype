using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MvcApp.DalEntities.Entities;

namespace MvcApp.Dal.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            ToTable("Группа");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id_группы").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.EntityVersion).IsRowVersion();
            Property(x => x.GroupNumber).HasColumnName("Номер_группы");
            HasOptional(x => x.Course).WithMany(x => x.Groups).Map(x => x.MapKey("Id_курса")).WillCascadeOnDelete(true);
            HasOptional(x => x.Teacher).WithMany(x => x.Groups).Map(x => x.MapKey("Id_преподавателя"));
        }
    }
}
