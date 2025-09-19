using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class FixedPropertyNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sender_TaxPayerId",
                table: "Senders");

            migrationBuilder.RenameColumn(
                name: "TaxPayerId",
                table: "Senders",
                newName: "TaxPayerNum");

            migrationBuilder.DropColumn(
                name: "EquipmentNumber",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "ManufactureDate",
                table: "Equipment");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Equipment",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Equipment",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturedYear",
                table: "Equipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sender_TaxPayerNum",
                table: "Senders",
                column: "TaxPayerNum",
                unique: true,
                filter: "\"TaxPayerNum\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sender_TaxPayerNum",
                table: "Senders");

            migrationBuilder.RenameColumn(
                name: "TaxPayerNum",
                table: "Senders",
                newName: "TaxPayerId");

            migrationBuilder.DropColumn(
                name: "ManufacturedYear",
                table: "Equipment");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Equipment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Equipment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "EquipmentNumber",
                table: "Equipment",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManufactureDate",
                table: "Equipment",
                type: "integer",
                maxLength: 4,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sender_TaxPayerId",
                table: "Senders",
                column: "TaxPayerId",
                unique: true,
                filter: "\"TaxPayerId\" IS NOT NULL");
        }
    }
}
