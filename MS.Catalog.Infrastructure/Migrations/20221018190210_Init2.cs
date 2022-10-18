using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Catalog.Infrastructure.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                schema: "catalog",
                table: "products",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "id",
                schema: "catalog",
                table: "products",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "varchar(36)");
        }
    }
}
