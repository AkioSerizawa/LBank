using LBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBank.Data.Mappings;

public class AccountTransactionMap : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.ToTable("AccountTransaction");

        builder.HasKey(x => x.TransactionId);
        builder.Property(x => x.TransactionId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.TransactionDocument)
            .IsRequired(false)
            .HasColumnName("TransactionDocument")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(35);

        builder.Property(x => x.TransactionHistory)
            .IsRequired(false)
            .HasColumnName("TransactionHistory")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(220);

        builder.Property(x => x.TransactionDate)
            .IsRequired()
            .HasColumnName("TransactionDate")
            .HasColumnType("DateTime");

        builder.Property(x => x.TransactionValue)
            .IsRequired()
            .HasColumnName("TransactionValue")
            .HasColumnType("Decimal");

        builder.HasOne(x => x.TransactionType)
            .WithMany(x => x.AccountTransaction)
            .HasForeignKey(x => x.TypeId)
            .HasConstraintName("FK_TRANSACTION_TYPE")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Account)
            .WithMany(x => x.AccountTransaction)
            .HasForeignKey(x => x.AccountId)
            .HasConstraintName("FK_TRANSACTION_ACCOUNT")
            .OnDelete(DeleteBehavior.Cascade);
    }
}