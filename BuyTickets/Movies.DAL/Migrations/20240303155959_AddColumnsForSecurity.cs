using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.DAL.Migrations
{
    public partial class AddColumnsForSecurity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAgree",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAgree",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Users");
        }
    }
}
