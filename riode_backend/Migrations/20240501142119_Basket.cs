using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace riode_backend.Migrations
{
    public partial class Basket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTopic_Blogs_BlogId",
                table: "BlogTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTopic_Topic_TopicId",
                table: "BlogTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTopic",
                table: "BlogTopic");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "BlogTopic",
                newName: "BlogTopics");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTopic_TopicId",
                table: "BlogTopics",
                newName: "IX_BlogTopics_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTopic_BlogId",
                table: "BlogTopics",
                newName: "IX_BlogTopics_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTopics",
                table: "BlogTopics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AppUserId",
                table: "BasketItems",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTopics_Blogs_BlogId",
                table: "BlogTopics",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTopics_Topics_TopicId",
                table: "BlogTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTopics_Blogs_BlogId",
                table: "BlogTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTopics_Topics_TopicId",
                table: "BlogTopics");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTopics",
                table: "BlogTopics");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "BlogTopics",
                newName: "BlogTopic");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTopics_TopicId",
                table: "BlogTopic",
                newName: "IX_BlogTopic_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTopics_BlogId",
                table: "BlogTopic",
                newName: "IX_BlogTopic_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTopic",
                table: "BlogTopic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTopic_Blogs_BlogId",
                table: "BlogTopic",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTopic_Topic_TopicId",
                table: "BlogTopic",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
