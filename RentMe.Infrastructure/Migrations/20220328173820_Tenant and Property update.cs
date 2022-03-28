using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMe.Infrastructure.Migrations
{
    public partial class TenantandPropertyupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Tenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Properties_PropertyId",
                table: "Tenants",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Properties_PropertyId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Properties");
        }
    }
}
