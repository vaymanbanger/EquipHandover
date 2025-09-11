using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDataTypesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxPayerId",
                table: "Senders",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Receivers",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 13);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TaxPayerId",
                table: "Senders",
                type: "integer",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationNumber",
                table: "Receivers",
                type: "integer",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);
        }
    }
}
