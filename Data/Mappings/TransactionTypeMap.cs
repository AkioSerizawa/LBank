using LBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBank.Data.Mappings;

public class TransactionTypeMap : IEntityTypeConfiguration<TransactionType>
{
    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder.ToTable("TransactionType");

        builder.HasKey(x => x.TypeId);
        builder.Property(x => x.TypeId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.TypeDescription)
            .IsRequired()
            .HasColumnName("TypeDescription")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(35);
    }
}