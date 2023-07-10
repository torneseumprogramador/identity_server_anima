using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace identityserveranima.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministratorSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdministratorId",
                table: "Administrators",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Administrators",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Administrators",
                newName: "AdministratorId");
        }
    }
}
