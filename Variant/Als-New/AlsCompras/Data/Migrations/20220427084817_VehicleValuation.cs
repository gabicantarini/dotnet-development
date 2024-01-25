using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlsCompras.Data.Migrations
{
    public partial class VehicleValuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleValuations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleEquipamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceValuation = table.Column<float>(type: "real", nullable: false),
                    CrmClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOrigin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleValuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleValuations_CrmClient_CrmClientId",
                        column: x => x.CrmClientId,
                        principalTable: "CrmClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleValuations_VehicleColor_VehicleColorId",
                        column: x => x.VehicleColorId,
                        principalTable: "VehicleColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleValuations_VehicleEquipament_VehicleEquipamentId",
                        column: x => x.VehicleEquipamentId,
                        principalTable: "VehicleEquipament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleValuations_CrmClientId",
                table: "VehicleValuations",
                column: "CrmClientId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleValuations_VehicleColorId",
                table: "VehicleValuations",
                column: "VehicleColorId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleValuations_VehicleEquipamentId",
                table: "VehicleValuations",
                column: "VehicleEquipamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleValuations");
        }
    }
}
