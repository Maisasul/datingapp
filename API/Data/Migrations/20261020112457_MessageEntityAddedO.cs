using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class MessageEntityAddedO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-id",
                column: "ConcurrencyStamp",
                value: "8cb75e00-ebb2-46fe-b43e-65daa84f5f97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "member-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "cfd38268-8ac1-46c6-8a96-714fd3e4e205", "MEMBER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "moderator-id",
                column: "ConcurrencyStamp",
                value: "4cc55e13-ebaa-445f-82af-351f103dbd5d");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-id",
                column: "ConcurrencyStamp",
                value: "fecc0739-1322-4460-abc4-63ff4c2719b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "member-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "bbdb76f2-5b25-45bf-8b43-f45af080cc78", "MEMEBER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "moderator-id",
                column: "ConcurrencyStamp",
                value: "d1f8f92c-dc35-4c2d-9da2-482c44d6efac");
        }
    }
}
