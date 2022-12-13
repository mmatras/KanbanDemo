using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoKanban.Migrations
{
    public partial class AuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Issues",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2023, 3, 13, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2022, 12, 16, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2023, 2, 1, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9793));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2023, 2, 20, 9, 36, 46, 497, DateTimeKind.Local).AddTicks(5352));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2022, 11, 23, 9, 36, 46, 497, DateTimeKind.Local).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2023, 1, 9, 9, 36, 46, 497, DateTimeKind.Local).AddTicks(5397));
        }
    }
}
