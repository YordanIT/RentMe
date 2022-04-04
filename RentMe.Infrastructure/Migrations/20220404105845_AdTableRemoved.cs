using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMe.Infrastructure.Migrations
{
    public partial class AdTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Ads_AdId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Images_AdId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Images",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "AdId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdId",
                table: "Images",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_PropertyId",
                table: "Ads",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Ads_AdId",
                table: "Images",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id");
        }
    }
}
