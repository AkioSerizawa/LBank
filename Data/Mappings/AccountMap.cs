using LBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBank.Data.Mappings;

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");

        builder.HasKey(x => x.AccountId);
        builder.Property(x => x.AccountId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.AccountBalance)
            .IsRequired()
            .HasColumnName("AccountBalance")
            .HasColumnType("Decimal");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Account)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_ACCOUNT_USER")
            .OnDelete(DeleteBehavior.Cascade);
    }
}