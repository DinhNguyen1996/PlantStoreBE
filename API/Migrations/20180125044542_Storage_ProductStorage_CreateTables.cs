using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Storage_ProductStorage_CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    StorageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStorages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductStorages_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_ProductId",
                table: "ProductStorages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_StorageId",
                table: "ProductStorages",
                column: "StorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStorages");

            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
