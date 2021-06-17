using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopProject.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Model = table.Column<long>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ImgUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangePassWords",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalPassword = table.Column<string>(nullable: true),
                    CheckOriginalPassword = table.Column<string>(nullable: true),
                    NewPassword = table.Column<string>(nullable: true),
                    ConfirmedPassword = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangePassWords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Admin = table.Column<bool>(nullable: false),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ReceiveName = table.Column<string>(nullable: false),
                    ReceivePhoneNumber = table.Column<string>(nullable: false),
                    ReceiveAddress = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Customers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Model = table.Column<long>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    ImgUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_productTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "productTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<Guid>(nullable: false),
                    ProductModel = table.Column<long>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Admin", "Birthday", "Email", "Gender", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, "333桃園市龜山區德明路5號", true, new DateTime(2021, 5, 27, 12, 47, 43, 555, DateTimeKind.Local).AddTicks(8881), "Admin@gmail.com", "Female", "Admin", "123", "0987654321" },
                    { 2, "333桃園市龜山區德明路5號", false, new DateTime(2021, 5, 27, 12, 47, 43, 556, DateTimeKind.Local).AddTicks(1486), "User@gmail.com", "Male", "User", "123", "0912345678" }
                });

            migrationBuilder.InsertData(
                table: "productTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Top" },
                    { 2, "Bottom" },
                    { 3, "Shoes" },
                    { 4, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreationDate", "ImgUrl", "Inventory", "Model", "Name", "Price", "TypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 27, 12, 47, 43, 551, DateTimeKind.Local).AddTicks(4086), "~/images/clothes/clothes1.jpg", 99, 2021052200001L, "White Short Jacket", 900, 1 },
                    { 2, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(672), "~/images/clothes/clothes2.jpg", 99, 2021052200002L, "White Short vest ", 500, 1 },
                    { 3, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(766), "~/images/clothes/clothes3.jpg", 99, 2021052200003L, "White Lace vest", 500, 1 },
                    { 4, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(772), "~/images/clothes/clothes4.jpg", 99, 2021052200004L, "Sleeveless vest", 700, 1 },
                    { 5, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(775), "~/images/bottom/bottom1.jpg", 99, 2021052200005L, "Gray Lace pants", 1200, 2 },
                    { 6, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(779), "~/images/bottom/bottom2.jpg", 99, 2021052200006L, "Black Satin Pants", 1200, 2 },
                    { 7, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(783), "~/images/bottom/bottom3.jpg", 99, 2021052200007L, "Yellow Plaid Pants", 1100, 2 },
                    { 8, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(787), "~/images/bottom/bottom4.jpg", 99, 2021052200008L, "Five-Point Jeans", 1200, 2 },
                    { 9, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(790), "~/images/shoes/shoes1.jpg", 99, 2021052200009L, "Red High Heels", 900, 3 },
                    { 10, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(793), "~/images/shoes/shoes2.jpg", 99, 2021052200010L, "White High Heels", 900, 3 },
                    { 11, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(796), "~/images/shoes/shoes3.jpg", 99, 2021052200011L, "Green High Heels", 900, 3 },
                    { 12, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(799), "~/images/shoes/shoes4.jpg", 99, 2021052200012L, "White Sneakers", 900, 3 },
                    { 13, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(802), "~/images/accessories/accessories1.jpg", 99, 2021052200013L, "Gold Retro Watch", 600, 4 },
                    { 14, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(805), "~/images/accessories/accessories2.jpg", 99, 2021052200014L, "Fimbrilla Earings", 500, 4 },
                    { 15, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(808), "~/images/accessories/accessories3.jpg", 99, 2021052200015L, "Leather Handbag", 800, 4 },
                    { 16, new DateTime(2021, 5, 27, 12, 47, 43, 553, DateTimeKind.Local).AddTicks(811), "~/images/accessories/accessories4.jpg", 99, 2021052200016L, "Retro Glasses", 800, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PurchaseId",
                table: "Orders",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId1",
                table: "Purchases",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ChangePassWords");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "productTypes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
