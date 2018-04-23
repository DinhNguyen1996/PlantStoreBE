using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_CustomerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    BirthDate = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: false),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyEmail = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyPhone = table.Column<string>(nullable: true),
                    CompanyTaxCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    ShipAddress = table.Column<string>(nullable: true),
                    ShipContactName = table.Column<string>(nullable: true),
                    ShipPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
