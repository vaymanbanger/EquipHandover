using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class SettingConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipment_Name",
                table: "Equipment");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Equipment_Name",
                table: "Equipment");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }
    }
}
