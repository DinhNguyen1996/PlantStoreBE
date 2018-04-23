using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Warehousing_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wareshousing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    DebtMoney = table.Column<double>(nullable: false),
                    DestroyDateTime = table.Column<DateTime>(nullable: false),
                    DestroyUserId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PaymentMoney = table.Column<double>(nullable: false),
                    ProductList = table.Column<string>(nullable: false),
                    StorageId = table.Column<Guid>(nullable: false),
                    SummaryMoney = table.Column<double>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wareshousing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wareshousing_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wareshousing_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wareshousing_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wareshousing_CreatedUserId",
                table: "Wareshousing",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wareshousing_StorageId",
                table: "Wareshousing",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Wareshousing_SupplierId",
                table: "Wareshousing",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wareshousing");
        }
    }
}
