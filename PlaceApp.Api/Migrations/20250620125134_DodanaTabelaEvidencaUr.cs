using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class DodanaTabelaEvidencaUr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvidenceUr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaposlenId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedneUre = table.Column<int>(type: "int", nullable: false),
                    Nadure = table.Column<int>(type: "int", nullable: false),
                    PraznikUre = table.Column<int>(type: "int", nullable: false),
                    DopustUre = table.Column<int>(type: "int", nullable: false),
                    BolniskaUre = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opomba = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidenceUr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvidenceUr_Zaposleni_ZaposlenId",
                        column: x => x.ZaposlenId,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceUr_ZaposlenId",
                table: "EvidenceUr",
                column: "ZaposlenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvidenceUr");
        }
    }
}
