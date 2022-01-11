using Microsoft.EntityFrameworkCore.Migrations;

namespace PD.Infrastructure.Migrations
{
    public partial class changedeliverymethodorderstatustointtyperenameIsPaidtoIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Orders",
                newName: "IsActive");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "2c65f0f9-cd27-4aaf-88c3-3fdf56a89b7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "cf5157b3-bd78-47b5-8b7d-da7359deefbe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Orders",
                newName: "IsPaid");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "fa497175-6051-49f7-81e5-dfc6648d3f2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "e3a5b039-4d5a-45fe-9089-f4d2a27af382");
        }
    }
}
