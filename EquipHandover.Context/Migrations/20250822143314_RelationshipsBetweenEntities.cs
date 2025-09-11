using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Documents_DocumentId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_DocumentId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Equipment");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_DeletedAt",
                table: "Equipment",
                newName: "IX_Equipment_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                newName: "IX_Equipment_DeletedAt");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Equipment",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_DocumentId",
                table: "Equipment",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Documents_DocumentId",
                table: "Equipment",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }
    }
}
