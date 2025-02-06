

using App.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class TaskUserConfiguration : IEntityTypeConfiguration<TaskU>
{
    public void Configure(EntityTypeBuilder<TaskU> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("TaskUsers");
        builder.Property(x => x.Title).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(4000);
        builder.HasOne(x => x.UserTa)
              .WithMany(x => x.TaskUs)
              .HasForeignKey(x => x.UserTaId)
              .OnDelete(DeleteBehavior.NoAction);
    }
}
