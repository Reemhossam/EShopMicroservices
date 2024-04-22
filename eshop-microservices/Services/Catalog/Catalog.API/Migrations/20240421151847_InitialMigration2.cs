using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageFile", "Name", "Price" },
                values: new object[] { new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "this phone is a new version of iphone", "product-1.png", "IPhone X", 950m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"));
        }
    }
}
