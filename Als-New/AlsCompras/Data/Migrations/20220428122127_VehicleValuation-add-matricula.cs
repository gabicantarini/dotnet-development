using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlsCompras.Data.Migrations
{
    public partial class VehicleValuationaddmatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_CrmClient_CrmClientId",
                table: "VehicleValuation");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_VehicleColor_VehicleColorId",
                table: "VehicleValuation");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuation");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleEquipamentId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleColorId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "TypeOrigin",
                table: "VehicleValuation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CrmClientId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "LicencePlate",
                table: "VehicleValuation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_CrmClient_CrmClientId",
                table: "VehicleValuation",
                column: "CrmClientId",
                principalTable: "CrmClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_VehicleColor_VehicleColorId",
                table: "VehicleValuation",
                column: "VehicleColorId",
                principalTable: "VehicleColor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuation",
                column: "VehicleEquipamentId",
                principalTable: "VehicleEquipament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_CrmClient_CrmClientId",
                table: "VehicleValuation");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_VehicleColor_VehicleColorId",
                table: "VehicleValuation");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuation_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuation");

            migrationBuilder.DropColumn(
                name: "LicencePlate",
                table: "VehicleValuation");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleEquipamentId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleColorId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeOrigin",
                table: "VehicleValuation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CrmClientId",
                table: "VehicleValuation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_CrmClient_CrmClientId",
                table: "VehicleValuation",
                column: "CrmClientId",
                principalTable: "CrmClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_VehicleColor_VehicleColorId",
                table: "VehicleValuation",
                column: "VehicleColorId",
                principalTable: "VehicleColor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuation_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuation",
                column: "VehicleEquipamentId",
                principalTable: "VehicleEquipament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
