using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serviteca.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddEntity50000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Soats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Soats");
        }
    }
}
