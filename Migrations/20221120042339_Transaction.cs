using Microsoft.EntityFrameworkCore.Migrations;

namespace LBank.Migrations
{
    public partial class Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTION_ACCOUNT",
                table: "AccountTransaction");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransaction_AccountId",
                table: "AccountTransaction");

            migrationBuilder.AlterColumn<string>(
                name: "UserSlug",
                table: "User",
                type: "NVARCHAR(65)",
                maxLength: 65,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(65)",
                oldMaxLength: 65);

            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "User",
                type: "NVARCHAR(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionValue",
                table: "AccountTransaction",
                type: "Decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionHistory",
                table: "AccountTransaction",
                type: "NVARCHAR(220)",
                maxLength: 220,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(220)",
                oldMaxLength: 220);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionDocument",
                table: "AccountTransaction",
                type: "NVARCHAR(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(35)",
                oldMaxLength: 35);

            migrationBuilder.AddColumn<int>(
                name: "AccountTransferId",
                table: "AccountTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                table: "Account",
                type: "Decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountTransferId",
                table: "AccountTransaction",
                column: "AccountTransferId");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSFER_ACCOUNT",
                table: "AccountTransaction",
                column: "AccountTransferId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSFER_ACCOUNT",
                table: "AccountTransaction");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransaction_AccountTransferId",
                table: "AccountTransaction");

            migrationBuilder.DropColumn(
                name: "AccountTransferId",
                table: "AccountTransaction");

            migrationBuilder.AlterColumn<string>(
                name: "UserSlug",
                table: "User",
                type: "NVARCHAR(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(65)",
                oldMaxLength: 65,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "User",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionValue",
                table: "AccountTransaction",
                type: "Decimal",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionHistory",
                table: "AccountTransaction",
                type: "NVARCHAR(220)",
                maxLength: 220,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(220)",
                oldMaxLength: 220,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionDocument",
                table: "AccountTransaction",
                type: "NVARCHAR(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                table: "Account",
                type: "Decimal",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountId",
                table: "AccountTransaction",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTION_ACCOUNT",
                table: "AccountTransaction",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
