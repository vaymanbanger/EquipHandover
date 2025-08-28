using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurationsSender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers");

            migrationBuilder.CreateIndex(
                name: "IX_Sender_TaxPayerId",
                table: "Senders",
                column: "TaxPayerId",
                unique: true,
                filter: "\"TaxPayerId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers",
                column: "RegistrationNumber",
                unique: true,
                filter: "\"RegistrationNumber\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sender_TaxPayerId",
                table: "Senders");

            migrationBuilder.DropIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers");

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_RegistrationNumber",
                table: "Receivers",
                column: "RegistrationNumber",
                unique: true,
                filter: "[Ogrn] IS NOT NULL");
        }
    }
}
