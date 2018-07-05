using Microsoft.EntityFrameworkCore.Migrations;

namespace Fiets.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_AspNetUsers_UserID",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_UserID",
                table: "Rides");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Rides",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Rides",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserID",
                table: "Rides",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_AspNetUsers_UserID",
                table: "Rides",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
