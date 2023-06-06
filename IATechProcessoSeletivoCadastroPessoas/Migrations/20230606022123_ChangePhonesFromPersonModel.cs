﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IATechProcessoSeletivoCadastroPessoas.Migrations
{
    public partial class ChangePhonesFromPersonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phone_People_PersonId",
                table: "Phone");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Phone",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_People_PersonId",
                table: "Phone",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phone_People_PersonId",
                table: "Phone");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Phone",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_People_PersonId",
                table: "Phone",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
