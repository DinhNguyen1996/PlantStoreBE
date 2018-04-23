using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Supplier_CreateTable_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
