using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoKanban.Migrations
{
    public partial class AddedFilesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2023, 5, 19, 13, 41, 26, 746, DateTimeKind.Local).AddTicks(9999));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2023, 2, 22, 13, 41, 26, 747, DateTimeKind.Local).AddTicks(106));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2023, 4, 10, 13, 41, 26, 747, DateTimeKind.Local).AddTicks(112));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateTime(2023, 3, 17, 14, 49, 21, 873, DateTimeKind.Local).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2022, 12, 20, 14, 49, 21, 873, DateTimeKind.Local).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deadline",
                value: new DateTime(2023, 2, 5, 14, 49, 21, 873, DateTimeKind.Local).AddTicks(67));
        }
    }
}
