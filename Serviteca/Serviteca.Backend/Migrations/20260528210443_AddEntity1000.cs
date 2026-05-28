using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serviteca.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddEntity1000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Brands_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Uses_VehicleUseId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleUseId",
                table: "Vehicles",
                newName: "UseId");

            migrationBuilder.RenameColumn(
                name: "VehicleTypeId",
                table: "Vehicles",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleUseId",
                table: "Vehicles",
                newName: "IX_Vehicles_UseId");

            migrationBuilder.AlterColumn<int>(
                name: "TypeVId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BrandId",
                table: "Vehicles",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Uses_UseId",
                table: "Vehicles",
                column: "UseId",
                principalTable: "Uses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Brands_BrandId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Uses_UseId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_BrandId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "UseId",
                table: "Vehicles",
                newName: "VehicleUseId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Vehicles",
                newName: "VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_UseId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleUseId");

            migrationBuilder.AlterColumn<int>(
                name: "TypeVId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Brands_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Uses_VehicleUseId",
                table: "Vehicles",
                column: "VehicleUseId",
                principalTable: "Uses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
