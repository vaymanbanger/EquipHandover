using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class FixedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inn",
                table: "Senders",
                newName: "TaxPayerId");

            migrationBuilder.RenameColumn(
                name: "Ogrn",
                table: "Receivers",
                newName: "RegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers",
                column: "RegistrationNumber",
                unique: true,
                filter: "[Ogrn] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers");

            migrationBuilder.RenameColumn(
                name: "TaxPayerId",
                table: "Senders",
                newName: "Inn");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Receivers",
                newName: "Ogrn");
        }
    }
}
