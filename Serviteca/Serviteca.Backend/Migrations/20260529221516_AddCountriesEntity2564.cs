using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serviteca.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCountriesEntity2564 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Vehicles");
        }
    }
}
