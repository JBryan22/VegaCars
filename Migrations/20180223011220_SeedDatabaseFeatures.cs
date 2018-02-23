using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VegaNew.Migrations
{
    public partial class SeedDatabaseFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.ID);
                });
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('GPS')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Premium Audio')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Sunroof')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
