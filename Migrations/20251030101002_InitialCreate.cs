using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CibusMed_App.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "john.smith@email.com", "John Smith" },
                    { 2, "sarah.johnson@email.com", "Sarah Johnson" },
                    { 3, "michael.brown@email.com", "Michael Brown" },
                    { 4, "emily.davis@email.com", "Emily Davis" },
                    { 5, "david.wilson@email.com", "David Wilson" },
                    { 6, "lisa.anderson@email.com", "Lisa Anderson" },
                    { 7, "james.taylor@email.com", "James Taylor" },
                    { 8, "maria.garcia@email.com", "Maria Garcia" },
                    { 9, "robert.martinez@email.com", "Robert Martinez" },
                    { 10, "jennifer.lee@email.com", "Jennifer Lee" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Sku", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Wireless Mouse", "PRD-001", 29.99m },
                    { 2, "Mechanical Keyboard", "PRD-002", 89.99m },
                    { 3, "USB-C Cable", "PRD-003", 12.99m },
                    { 4, "Monitor Stand", "PRD-004", 45.99m },
                    { 5, "Webcam HD", "PRD-005", 59.99m },
                    { 6, "Headset", "PRD-006", 79.99m },
                    { 7, "Laptop Sleeve", "PRD-007", 24.99m },
                    { 8, "Desk Lamp", "PRD-008", 34.99m },
                    { 9, "External SSD 1TB", "PRD-009", 129.99m },
                    { 10, "Ergonomic Chair Cushion", "PRD-010", 39.99m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAtUtc", "CustomerId", "Notes", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 31, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(2989), 1, "Test order in Pending status", 0 },
                    { 2, new DateTime(2025, 10, 17, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3156), 2, "Test order in Approved status", 1 },
                    { 3, new DateTime(2025, 10, 18, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3162), 3, "Test order in Shipped status", 2 },
                    { 4, new DateTime(2025, 9, 13, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3166), 4, "Test order in Cancelled status", 3 },
                    { 5, new DateTime(2025, 10, 15, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3195), 5, "Order for customer 5", 1 },
                    { 6, new DateTime(2025, 8, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3215), 6, "Order for customer 6", 2 },
                    { 7, new DateTime(2025, 10, 14, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3221), 7, "Order for customer 7", 3 },
                    { 8, new DateTime(2025, 10, 9, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3226), 8, "Order for customer 8", 1 },
                    { 9, new DateTime(2025, 9, 15, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3232), 9, "Order for customer 9", 1 },
                    { 10, new DateTime(2025, 9, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3239), 10, "Order for customer 10", 1 },
                    { 11, new DateTime(2025, 10, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3248), 6, null, 3 },
                    { 12, new DateTime(2025, 10, 16, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3251), 4, null, 0 },
                    { 13, new DateTime(2025, 9, 11, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3252), 9, null, 0 },
                    { 14, new DateTime(2025, 8, 10, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3254), 2, null, 2 },
                    { 15, new DateTime(2025, 10, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3255), 2, null, 1 },
                    { 16, new DateTime(2025, 10, 29, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3257), 9, null, 2 },
                    { 17, new DateTime(2025, 9, 25, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3258), 8, "Order #17", 0 },
                    { 18, new DateTime(2025, 8, 21, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3264), 1, "Order #18", 2 },
                    { 19, new DateTime(2025, 8, 22, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3267), 3, null, 1 },
                    { 20, new DateTime(2025, 10, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3269), 2, null, 2 },
                    { 21, new DateTime(2025, 9, 22, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3270), 1, null, 2 },
                    { 22, new DateTime(2025, 10, 28, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3272), 3, "Order #22", 1 },
                    { 23, new DateTime(2025, 8, 27, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3275), 1, "Order #23", 0 },
                    { 24, new DateTime(2025, 8, 19, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3277), 1, null, 1 },
                    { 25, new DateTime(2025, 10, 29, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3279), 5, "Order #25", 3 },
                    { 26, new DateTime(2025, 10, 24, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3282), 1, "Order #26", 2 },
                    { 27, new DateTime(2025, 8, 8, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3285), 10, "Order #27", 1 },
                    { 28, new DateTime(2025, 10, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3287), 7, "Order #28", 1 },
                    { 29, new DateTime(2025, 8, 20, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3290), 5, "Order #29", 2 },
                    { 30, new DateTime(2025, 8, 10, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3293), 3, null, 2 },
                    { 31, new DateTime(2025, 8, 12, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3294), 1, "Order #31", 3 },
                    { 32, new DateTime(2025, 10, 15, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3297), 8, "Order #32", 0 },
                    { 33, new DateTime(2025, 8, 21, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3300), 4, "Order #33", 0 },
                    { 34, new DateTime(2025, 9, 7, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3303), 3, null, 2 },
                    { 35, new DateTime(2025, 8, 31, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3305), 8, "Order #35", 2 },
                    { 36, new DateTime(2025, 8, 2, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3308), 6, "Order #36", 2 },
                    { 37, new DateTime(2025, 8, 8, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3311), 4, "Order #37", 0 },
                    { 38, new DateTime(2025, 10, 26, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3313), 3, null, 0 },
                    { 39, new DateTime(2025, 10, 21, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3315), 5, "Order #39", 3 },
                    { 40, new DateTime(2025, 10, 1, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3318), 2, null, 1 },
                    { 41, new DateTime(2025, 10, 29, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3319), 9, null, 0 },
                    { 42, new DateTime(2025, 10, 8, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3321), 9, null, 3 },
                    { 43, new DateTime(2025, 9, 11, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3323), 3, null, 2 },
                    { 44, new DateTime(2025, 10, 3, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3364), 2, "Order #44", 0 },
                    { 45, new DateTime(2025, 8, 7, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3368), 3, null, 3 },
                    { 46, new DateTime(2025, 8, 12, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3369), 2, "Order #46", 0 },
                    { 47, new DateTime(2025, 9, 10, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3372), 6, "Order #47", 2 },
                    { 48, new DateTime(2025, 9, 25, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3375), 6, "Order #48", 3 },
                    { 49, new DateTime(2025, 8, 5, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3378), 4, null, 1 },
                    { 50, new DateTime(2025, 8, 18, 10, 10, 1, 435, DateTimeKind.Utc).AddTicks(3379), 10, "Order #50", 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 5, 89.99m },
                    { 2, 2, 5, 3, 59.99m },
                    { 3, 2, 2, 5, 89.99m },
                    { 4, 2, 10, 5, 39.99m },
                    { 5, 3, 4, 3, 45.99m },
                    { 6, 4, 10, 5, 39.99m },
                    { 7, 4, 8, 2, 34.99m },
                    { 8, 4, 4, 3, 45.99m },
                    { 9, 5, 3, 5, 12.99m },
                    { 10, 5, 7, 1, 24.99m },
                    { 11, 5, 9, 4, 129.99m },
                    { 12, 5, 1, 3, 29.99m },
                    { 13, 6, 9, 4, 129.99m },
                    { 14, 6, 10, 5, 39.99m },
                    { 15, 6, 5, 5, 59.99m },
                    { 16, 6, 2, 3, 89.99m },
                    { 17, 7, 7, 3, 24.99m },
                    { 18, 8, 2, 5, 89.99m },
                    { 19, 8, 4, 3, 45.99m },
                    { 20, 8, 8, 2, 34.99m },
                    { 21, 9, 8, 3, 34.99m },
                    { 22, 9, 2, 5, 89.99m },
                    { 23, 9, 1, 1, 29.99m },
                    { 24, 10, 4, 1, 45.99m },
                    { 25, 11, 3, 5, 12.99m },
                    { 26, 11, 5, 2, 59.99m },
                    { 27, 12, 8, 4, 34.99m },
                    { 28, 12, 10, 2, 39.99m },
                    { 29, 13, 4, 1, 45.99m },
                    { 30, 13, 10, 3, 39.99m },
                    { 31, 13, 7, 2, 24.99m },
                    { 32, 13, 1, 3, 29.99m },
                    { 33, 14, 4, 5, 45.99m },
                    { 34, 14, 9, 5, 129.99m },
                    { 35, 14, 1, 2, 29.99m },
                    { 36, 14, 6, 1, 79.99m },
                    { 37, 15, 5, 4, 59.99m },
                    { 38, 15, 7, 5, 24.99m },
                    { 39, 15, 3, 5, 12.99m },
                    { 40, 15, 8, 1, 34.99m },
                    { 41, 16, 7, 1, 24.99m },
                    { 42, 16, 6, 2, 79.99m },
                    { 43, 17, 3, 4, 12.99m },
                    { 44, 17, 7, 4, 24.99m },
                    { 45, 18, 7, 1, 24.99m },
                    { 46, 18, 2, 5, 89.99m },
                    { 47, 19, 10, 2, 39.99m },
                    { 48, 19, 1, 3, 29.99m },
                    { 49, 20, 2, 1, 89.99m },
                    { 50, 20, 7, 1, 24.99m },
                    { 51, 21, 2, 4, 89.99m },
                    { 52, 21, 7, 4, 24.99m },
                    { 53, 21, 9, 2, 129.99m },
                    { 54, 21, 5, 3, 59.99m },
                    { 55, 22, 8, 3, 34.99m },
                    { 56, 23, 7, 3, 24.99m },
                    { 57, 23, 10, 4, 39.99m },
                    { 58, 23, 3, 2, 12.99m },
                    { 59, 24, 9, 4, 129.99m },
                    { 60, 24, 4, 3, 45.99m },
                    { 61, 24, 8, 3, 34.99m },
                    { 62, 25, 6, 5, 79.99m },
                    { 63, 25, 1, 3, 29.99m },
                    { 64, 25, 5, 1, 59.99m },
                    { 65, 25, 10, 3, 39.99m },
                    { 66, 26, 5, 4, 59.99m },
                    { 67, 26, 1, 1, 29.99m },
                    { 68, 27, 5, 5, 59.99m },
                    { 69, 27, 10, 3, 39.99m },
                    { 70, 27, 3, 5, 12.99m },
                    { 71, 27, 9, 4, 129.99m },
                    { 72, 28, 9, 3, 129.99m },
                    { 73, 28, 4, 1, 45.99m },
                    { 74, 28, 2, 5, 89.99m },
                    { 75, 29, 10, 3, 39.99m },
                    { 76, 29, 2, 2, 89.99m },
                    { 77, 29, 4, 1, 45.99m },
                    { 78, 30, 7, 1, 24.99m },
                    { 79, 31, 10, 5, 39.99m },
                    { 80, 32, 4, 3, 45.99m },
                    { 81, 32, 2, 4, 89.99m },
                    { 82, 32, 3, 3, 12.99m },
                    { 83, 32, 1, 1, 29.99m },
                    { 84, 33, 1, 3, 29.99m },
                    { 85, 33, 8, 3, 34.99m },
                    { 86, 33, 10, 4, 39.99m },
                    { 87, 34, 4, 3, 45.99m },
                    { 88, 34, 9, 4, 129.99m },
                    { 89, 34, 10, 5, 39.99m },
                    { 90, 34, 7, 1, 24.99m },
                    { 91, 35, 10, 5, 39.99m },
                    { 92, 35, 2, 4, 89.99m },
                    { 93, 35, 9, 5, 129.99m },
                    { 94, 35, 5, 5, 59.99m },
                    { 95, 36, 3, 5, 12.99m },
                    { 96, 37, 6, 5, 79.99m },
                    { 97, 37, 10, 1, 39.99m },
                    { 98, 38, 4, 5, 45.99m },
                    { 99, 38, 2, 5, 89.99m },
                    { 100, 38, 9, 3, 129.99m },
                    { 101, 38, 10, 1, 39.99m },
                    { 102, 39, 7, 2, 24.99m },
                    { 103, 39, 2, 1, 89.99m },
                    { 104, 39, 8, 1, 34.99m },
                    { 105, 40, 5, 2, 59.99m },
                    { 106, 41, 10, 4, 39.99m },
                    { 107, 41, 3, 5, 12.99m },
                    { 108, 41, 5, 4, 59.99m },
                    { 109, 42, 1, 2, 29.99m },
                    { 110, 42, 9, 5, 129.99m },
                    { 111, 42, 10, 2, 39.99m },
                    { 112, 43, 5, 5, 59.99m },
                    { 113, 44, 1, 5, 29.99m },
                    { 114, 44, 5, 5, 59.99m },
                    { 115, 44, 8, 5, 34.99m },
                    { 116, 44, 3, 5, 12.99m },
                    { 117, 45, 7, 4, 24.99m },
                    { 118, 46, 8, 3, 34.99m },
                    { 119, 46, 7, 2, 24.99m },
                    { 120, 47, 2, 1, 89.99m },
                    { 121, 47, 5, 3, 59.99m },
                    { 122, 48, 5, 4, 59.99m },
                    { 123, 48, 2, 4, 89.99m },
                    { 124, 49, 4, 5, 45.99m },
                    { 125, 49, 10, 2, 39.99m },
                    { 126, 49, 3, 2, 12.99m },
                    { 127, 49, 5, 3, 59.99m },
                    { 128, 50, 2, 5, 89.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
