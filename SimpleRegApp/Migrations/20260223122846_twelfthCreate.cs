using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleRegApp.Migrations
{
    /// <inheritdoc />
    public partial class twelfthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Events",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Events",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmPassword",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
