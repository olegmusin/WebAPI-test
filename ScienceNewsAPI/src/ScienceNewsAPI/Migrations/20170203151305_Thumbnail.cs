using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScienceNewsAPI.Migrations
{
    public partial class Thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Url = table.Column<string>(nullable: false),
                    Height = table.Column<short>(nullable: false),
                    Width = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Url);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ThumbnailUrl",
                table: "Items",
                column: "ThumbnailUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Thumbnail_ThumbnailUrl",
                table: "Items",
                column: "ThumbnailUrl",
                principalTable: "Thumbnail",
                principalColumn: "Url",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Thumbnail_ThumbnailUrl",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Items_ThumbnailUrl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Items");
        }
    }
}
