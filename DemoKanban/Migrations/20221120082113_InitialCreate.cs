using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoKanban.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedToId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_People_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AssignedToId", "Deadline", "IsUrgent", "Notes", "State", "Title" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2023, 1, 9, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9570), false, "Mamy dwa projekty do wyboru lub można wybrać swój", 0, "Zrobić samodzielny proejkt" },
                    { 4, null, null, false, "Język SQL jest językiem deklaratywnym", 2, "Nauczyć się RDBMS" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "DisplayName", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "matras", "Daniel", "Matras" },
                    { 2, "", "Marcin", "Nowak" },
                    { 3, "opolski", "Jan", "Opolski" },
                    { 4, "jdąb", "Magdalena", "Dąbrowska" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AssignedToId", "Deadline", "IsUrgent", "Notes", "State", "Title" },
                values: new object[] { 1, 1, new DateTime(2023, 2, 20, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9529), true, "Ten temat musi być bardzo dobrze opanowany", 0, "Nauczyć się C# oraz .NET" });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AssignedToId", "Deadline", "IsUrgent", "Notes", "State", "Title" },
                values: new object[] { 2, 3, new DateTime(2022, 11, 23, 9, 21, 13, 451, DateTimeKind.Local).AddTicks(9567), false, "Rwównież Razor Pages", 1, "Nauczyć się ASP.NET MVC" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues",
                column: "AssignedToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
