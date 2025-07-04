using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Product_Catalog_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class test100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03095064-53a9-45b1-b382-11f965dd8a8a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc1e7aa1-ae01-4a0b-b341-d83e254527d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79b52c5e-081f-4666-86b7-968c9744f9e0", null, "User", "USER" },
                    { "94c30b42-c0b2-4d4d-9b0a-10a7a417e949", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79b52c5e-081f-4666-86b7-968c9744f9e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94c30b42-c0b2-4d4d-9b0a-10a7a417e949");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03095064-53a9-45b1-b382-11f965dd8a8a", null, "User", "USER" },
                    { "fc1e7aa1-ae01-4a0b-b341-d83e254527d9", null, "Admin", "ADMIN" }
                });
        }
    }
}
