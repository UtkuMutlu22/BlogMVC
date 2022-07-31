using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogMVC.Migrations
{
    public partial class BlogChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BlogId",
                table: "Blogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogId",
                table: "Blogs",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Blogs_BlogId",
                table: "Blogs",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Blogs_BlogId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Blogs");
        }
    }
}
