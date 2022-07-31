using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogMVC.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RosetteId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RosetteId",
                table: "AspNetUsers",
                column: "RosetteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rosettes_RosetteId",
                table: "AspNetUsers",
                column: "RosetteId",
                principalTable: "Rosettes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rosettes_RosetteId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RosetteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RosetteId",
                table: "AspNetUsers");
        }
    }
}
