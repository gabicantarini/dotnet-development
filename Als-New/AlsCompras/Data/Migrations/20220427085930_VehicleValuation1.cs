using Microsoft.EntityFrameworkCore.Migrations;

namespace AlsCompras.Data.Migrations
{
    public partial class VehicleValuation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuations_CrmClient_CrmClientId",
                table: "VehicleValuations");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuations_VehicleColor_VehicleColorId",
                table: "VehicleValuations");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleValuations_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleValuations",
                table: "VehicleValuations");

            migrationBuilder.RenameTable(
                name: "VehicleValuations",
                newName: "VehicleValuation");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuations_VehicleEquipamentId",
                table: "VehicleValuation",
                newName: "IX_VehicleValuation_VehicleEquipamentId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuations_VehicleColorId",
                table: "VehicleValuation",
                newName: "IX_VehicleValuation_VehicleColorId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuations_CrmClientId",
                table: "VehicleValuation",
                newName: "IX_VehicleValuation_CrmClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleValuation",
                table: "VehicleValuation",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleValuation",
                table: "VehicleValuation");

            migrationBuilder.RenameTable(
                name: "VehicleValuation",
                newName: "VehicleValuations");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuation_VehicleEquipamentId",
                table: "VehicleValuations",
                newName: "IX_VehicleValuations_VehicleEquipamentId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuation_VehicleColorId",
                table: "VehicleValuations",
                newName: "IX_VehicleValuations_VehicleColorId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleValuation_CrmClientId",
                table: "VehicleValuations",
                newName: "IX_VehicleValuations_CrmClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleValuations",
                table: "VehicleValuations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuations_CrmClient_CrmClientId",
                table: "VehicleValuations",
                column: "CrmClientId",
                principalTable: "CrmClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuations_VehicleColor_VehicleColorId",
                table: "VehicleValuations",
                column: "VehicleColorId",
                principalTable: "VehicleColor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleValuations_VehicleEquipament_VehicleEquipamentId",
                table: "VehicleValuations",
                column: "VehicleEquipamentId",
                principalTable: "VehicleEquipament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
