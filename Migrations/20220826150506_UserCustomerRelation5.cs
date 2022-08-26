using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class UserCustomerRelation5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_GroomerIdId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "GroomerIdId",
                table: "Customers",
                newName: "GroomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_GroomerIdId",
                table: "Customers",
                newName: "IX_Customers_GroomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_GroomerId",
                table: "Customers",
                column: "GroomerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_GroomerId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "GroomerId",
                table: "Customers",
                newName: "GroomerIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_GroomerId",
                table: "Customers",
                newName: "IX_Customers_GroomerIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_GroomerIdId",
                table: "Customers",
                column: "GroomerIdId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
