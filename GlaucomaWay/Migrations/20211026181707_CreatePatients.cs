using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlaucomaWay.Migrations
{
    public partial class CreatePatients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vf14Results");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Vf14Results",
                type: "int",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BithDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vf14Results_PatientId",
                table: "Vf14Results",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vf14Results_Patients_PatientId",
                table: "Vf14Results",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vf14Results_Patients_PatientId",
                table: "Vf14Results");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Vf14Results_PatientId",
                table: "Vf14Results");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Vf14Results");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vf14Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
