using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VseTShirts.DB.Migrations
{
    /// <inheritdoc />
    public partial class addCollectionNameTOProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOfCollection",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("92bced76-82ba-4f44-af74-70eb7b31a6f9"),
                column: "NameOfCollection",
                value: "Коллекция1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ba7aec10-45d0-49ad-8ee6-ddbe95371796"),
                column: "NameOfCollection",
                value: "Коллекция1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfCollection",
                table: "Products");
        }
    }
}
