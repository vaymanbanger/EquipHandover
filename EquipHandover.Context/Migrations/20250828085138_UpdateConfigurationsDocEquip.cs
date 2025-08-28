using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurationsDocEquip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentEquipment",
                table: "DocumentEquipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentEquipment",
                table: "DocumentEquipment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentEquipment_DocumentId",
                table: "DocumentEquipment",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentEquipment",
                table: "DocumentEquipment");

            migrationBuilder.DropIndex(
                name: "IX_DocumentEquipment_DocumentId",
                table: "DocumentEquipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentEquipment",
                table: "DocumentEquipment",
                columns: new[] { "DocumentId", "EquipmentId" });
        }
    }
}
