using Microsoft.EntityFrameworkCore.Migrations;

namespace GlaucomaWay.Migrations
{
    public partial class RenameMisspelledField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Q51Score",
                table: "Vf14Results",
                newName: "Q5Score");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Q5Score",
                table: "Vf14Results",
                newName: "Q51Score");
        }
    }
}
