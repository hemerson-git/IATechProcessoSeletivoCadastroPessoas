using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IATechProcessoSeletivoCadastroPessoas.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_People_PersonModelId",
                        column: x => x.PersonModelId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CPF",
                table: "People",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PersonModelId",
                table: "Phone",
                column: "PersonModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
