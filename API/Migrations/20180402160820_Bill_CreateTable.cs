using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Bill_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DestroyDateTime = table.Column<DateTime>(nullable: false),
                    DestroyUserId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRetail = table.Column<bool>(nullable: false),
                    ProductList = table.Column<string>(nullable: false),
                    StorageId = table.Column<Guid>(nullable: false),
                    TotalMoney = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CreatedUserId",
                table: "Bill",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerId",
                table: "Bill",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_StorageId",
                table: "Bill",
                column: "StorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");
        }
    }
}
