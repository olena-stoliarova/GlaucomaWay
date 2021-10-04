using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlaucomaWay.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vf14Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Q1Score = table.Column<int>(type: "int", nullable: false),
                    Q2Score = table.Column<int>(type: "int", nullable: false),
                    Q3Score = table.Column<int>(type: "int", nullable: false),
                    Q4Score = table.Column<int>(type: "int", nullable: false),
                    Q51Score = table.Column<int>(type: "int", nullable: false),
                    Q6Score = table.Column<int>(type: "int", nullable: false),
                    Q7Score = table.Column<int>(type: "int", nullable: false),
                    Q8Score = table.Column<int>(type: "int", nullable: false),
                    Q9Score = table.Column<int>(type: "int", nullable: false),
                    Q10Score = table.Column<int>(type: "int", nullable: false),
                    Q11Score = table.Column<int>(type: "int", nullable: false),
                    Q12Score = table.Column<int>(type: "int", nullable: false),
                    Q13Score = table.Column<int>(type: "int", nullable: false),
                    Q14Score = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vf14Results", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vf14Results");
        }
    }
}
