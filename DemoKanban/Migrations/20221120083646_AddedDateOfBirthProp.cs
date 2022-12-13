using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoKanban.Migrations
{
    public partial class AddedDateOfBirthProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "People");

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2023, 2, 20, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2022, 11, 23, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9567));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2023, 1, 9, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9570));
        }
    }
}
