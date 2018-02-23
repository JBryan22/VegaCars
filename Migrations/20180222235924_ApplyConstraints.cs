using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VegaNew.Migrations
{
    public partial class ApplyConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_Makes_MakeID",
                table: "Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                table: "Model");

            migrationBuilder.RenameTable(
                name: "Model",
                newName: "Models");

            migrationBuilder.RenameIndex(
                name: "IX_Model_MakeID",
                table: "Models",
                newName: "IX_Models_MakeID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Models",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Makes",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models",
                column: "MakeID",
                principalTable: "Makes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Makes_MakeID",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model");

            migrationBuilder.RenameIndex(
                name: "IX_Models_MakeID",
                table: "Model",
                newName: "IX_Model_MakeID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Model",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Makes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                table: "Model",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Makes_MakeID",
                table: "Model",
                column: "MakeID",
                principalTable: "Makes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
