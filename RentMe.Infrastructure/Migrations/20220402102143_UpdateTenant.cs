using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentMe.Infrastructure.Migrations
{
    public partial class UpdateTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Tenants_TenantId",
                table: "Expenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Tenants_TenantId",
                table: "Expenses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Tenants_TenantId",
                table: "Expenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Tenants_TenantId",
                table: "Expenses",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
