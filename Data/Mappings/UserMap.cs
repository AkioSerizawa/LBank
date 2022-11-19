using LBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBank.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnName("UserName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.UserEmail)
            .IsRequired()
            .HasColumnName("UserEmail")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(220);

        builder.Property(x => x.UserSlug)
            .HasColumnName("UserSlug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(65);

        builder.Property(x => x.UserPassword)
            .IsRequired()
            .HasColumnName("UserPassword")
            .HasColumnType("NVARCHAR");
    }
}