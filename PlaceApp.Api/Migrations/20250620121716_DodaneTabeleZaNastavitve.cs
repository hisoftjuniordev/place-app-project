using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class DodaneTabeleZaNastavitve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DohodninskeLestvice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaporednaSt = table.Column<int>(type: "int", nullable: false),
                    MejaDo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stopnja = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AbsolutniZnesek = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DohodninskeLestvice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NastavitveObracuna",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeljavnoOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinimalnaPlaca = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenaPrehrane = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenaPrevozaKm = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NastavitveObracuna", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DohodninskeLestvice");

            migrationBuilder.DropTable(
                name: "NastavitveObracuna");
        }
    }
}
