using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipHandover.Context.Migrations
{
    /// <inheritdoc />
    public partial class TypeChangeSignatureNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем колонку (безопасно, так как данных нет)
            migrationBuilder.DropColumn(
                name: "SignatureNumber",
                table: "Documents");

            // Добавляем колонку с новым типом
            migrationBuilder.AddColumn<DateOnly>(
                name: "SignatureNumber",
                table: "Documents",
                type: "date",
                maxLength: 50,
                nullable: false,
                defaultValue: new DateOnly(1900, 1, 1));
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignatureNumber",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "SignatureNumber",
                table: "Documents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
