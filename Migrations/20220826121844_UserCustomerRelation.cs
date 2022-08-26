using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class UserCustomerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroomerID",
                table: "Customers",
                newName: "GroomerId");

            migrationBuilder.AlterColumn<int>(
                name: "GroomerId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GroomerId",
                table: "Customers",
                column: "GroomerId");

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

            migrationBuilder.DropIndex(
                name: "IX_Customers_GroomerId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "GroomerId",
                table: "Customers",
                newName: "GroomerID");

            migrationBuilder.AlterColumn<int>(
                name: "GroomerID",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
