using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class WarehousingReturn_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehousingReturn",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ProductList = table.Column<string>(nullable: false),
                    ReturnDateTime = table.Column<DateTime>(nullable: false),
                    ReturnUserId = table.Column<Guid>(nullable: false),
                    StorageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousingReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehousingReturn_AspNetUsers_ReturnUserId",
                        column: x => x.ReturnUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehousingReturn_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehousingReturn_ReturnUserId",
                table: "WarehousingReturn",
                column: "ReturnUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousingReturn_StorageId",
                table: "WarehousingReturn",
                column: "StorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehousingReturn");
        }
    }
}
