using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PD.Infrastructure.Migrations
{
    public partial class addpizzasinordercustomtableaddpizzapriceaddpromocodeexprdatechangediscountamountfromfloattobyte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza");

            migrationBuilder.DropTable(
                name: "OrderPizza");

            migrationBuilder.RenameColumn(
                name: "PizzasId",
                table: "IngredientPizza",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "IngredientsId",
                table: "IngredientPizza",
                newName: "PizzaId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_IngredientId");

            migrationBuilder.AlterColumn<byte>(
                name: "DiscountAmount",
                table: "PromoCodes",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "PromoCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Pizzas",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "PizzaOrder",
                columns: table => new
                {
                    PizzaId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaOrder", x => new { x.OrderId, x.PizzaId });
                    table.ForeignKey(
                        name: "FK_PizzaOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaOrder_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "d537f98e-395a-46bb-94c2-64aafc48452f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "06cc0ff0-2da0-4ec0-b558-adeb1efa08e9");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Cheese" },
                    { 2L, "Tomato" },
                    { 3L, "Pizza Base" },
                    { 4L, "Olives" }
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, "A lot of cheese", "Cheese pizza", 100f },
                    { 2L, "A lot of tomatoes", "Tomato pizza", 200f },
                    { 3L, "A lot of olives", "Pizza with olives", 300f }
                });

            migrationBuilder.InsertData(
                table: "IngredientPizza",
                columns: new[] { "IngredientId", "PizzaId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 3L, 1L },
                    { 2L, 2L },
                    { 3L, 2L },
                    { 4L, 3L },
                    { 3L, 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaOrder_PizzaId",
                table: "PizzaOrder",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientId",
                table: "IngredientPizza",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzaId",
                table: "IngredientPizza",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientId",
                table: "IngredientPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzaId",
                table: "IngredientPizza");

            migrationBuilder.DropTable(
                name: "PizzaOrder");

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 3L, 1L });

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 3L, 2L });

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 3L, 3L });

            migrationBuilder.DeleteData(
                table: "IngredientPizza",
                keyColumns: new[] { "IngredientId", "PizzaId" },
                keyValues: new object[] { 4L, 3L });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Pizzas");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "IngredientPizza",
                newName: "PizzasId");

            migrationBuilder.RenameColumn(
                name: "PizzaId",
                table: "IngredientPizza",
                newName: "IngredientsId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_IngredientId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_PizzasId");

            migrationBuilder.AlterColumn<float>(
                name: "DiscountAmount",
                table: "PromoCodes",
                type: "real",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateTable(
                name: "OrderPizza",
                columns: table => new
                {
                    OrdersId = table.Column<long>(type: "bigint", nullable: false),
                    PizzasId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPizza", x => new { x.OrdersId, x.PizzasId });
                    table.ForeignKey(
                        name: "FK_OrderPizza_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPizza_Pizzas_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "9d1abc12-b157-4a61-928e-2e7facd239fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "0dc96337-b650-49f4-9170-6a4506a4b672");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPizza_PizzasId",
                table: "OrderPizza",
                column: "PizzasId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsId",
                table: "IngredientPizza",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
