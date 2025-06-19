using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Podjetja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DavcnaStevilka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaticnaStevilka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podjetja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Obracuni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodjetjeID = table.Column<int>(type: "int", nullable: false),
                    Mesec = table.Column<int>(type: "int", nullable: false),
                    Leto = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIzdelave = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obracuni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obracuni_Podjetja_PodjetjeID",
                        column: x => x.PodjetjeID,
                        principalTable: "Podjetja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodjetjeID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMSO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DavcnaStevilka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumZaposlitve = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPrenehanja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JeAktiven = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposleni_Podjetja_PodjetjeID",
                        column: x => x.PodjetjeID,
                        principalTable: "Podjetja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odtegljaji",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaposleniID = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkupniZnesek = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MesecniObrok = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ZacetekOdplacevanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KonecOdplacevanja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JeAktiven = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odtegljaji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odtegljaji_Zaposleni_ZaposleniID",
                        column: x => x.ZaposleniID,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaposlenId = table.Column<int>(type: "int", nullable: false),
                    Obdobje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrnaPostavka = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    RednoDeloUre = table.Column<int>(type: "int", nullable: false),
                    PlacaniPraznikiUre = table.Column<int>(type: "int", nullable: false),
                    Poracun = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stimulacija = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DelovnaDobaProcent = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    PosebneOlajsave = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Odtegljaji = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrehranaDni = table.Column<int>(type: "int", nullable: false),
                    PrehranaZnesek = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrevozKm = table.Column<int>(type: "int", nullable: false),
                    KmCena = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    KilometrineFiksno = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BrutoPlaca = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetoPlaca = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dohodnina = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ZaIzplacilo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CelotenStrosekDelodajalca = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Place_Zaposleni_ZaposlenId",
                        column: x => x.ZaposlenId,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacilneListe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObracunID = table.Column<int>(type: "int", nullable: false),
                    ZaposleniID = table.Column<int>(type: "int", nullable: false),
                    SteviloUr = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UreOdsotnosti = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SkupajBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetoZaIzplacilo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacilneListe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlacilneListe_Obracuni_ObracunID",
                        column: x => x.ObracunID,
                        principalTable: "Obracuni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacilneListe_Zaposleni_ZaposleniID",
                        column: x => x.ZaposleniID,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pogodbe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZaposleniID = table.Column<int>(type: "int", nullable: false),
                    BrutoPlaca = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ZacetekVeljavnosti = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KonecVeljavnosti = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DodatekMinuloDelo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pogodbe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pogodbe_Zaposleni_ZaposleniID",
                        column: x => x.ZaposleniID,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostavkePlacilneListe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacilnaListaID = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipPostavke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Znesek = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostavkePlacilneListe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostavkePlacilneListe_PlacilneListe_PlacilnaListaID",
                        column: x => x.PlacilnaListaID,
                        principalTable: "PlacilneListe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obracuni_PodjetjeID",
                table: "Obracuni",
                column: "PodjetjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Odtegljaji_ZaposleniID",
                table: "Odtegljaji",
                column: "ZaposleniID");

            migrationBuilder.CreateIndex(
                name: "IX_Place_ZaposlenId",
                table: "Place",
                column: "ZaposlenId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacilneListe_ObracunID",
                table: "PlacilneListe",
                column: "ObracunID");

            migrationBuilder.CreateIndex(
                name: "IX_PlacilneListe_ZaposleniID",
                table: "PlacilneListe",
                column: "ZaposleniID");

            migrationBuilder.CreateIndex(
                name: "IX_Pogodbe_ZaposleniID",
                table: "Pogodbe",
                column: "ZaposleniID");

            migrationBuilder.CreateIndex(
                name: "IX_PostavkePlacilneListe_PlacilnaListaID",
                table: "PostavkePlacilneListe",
                column: "PlacilnaListaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_PodjetjeID",
                table: "Zaposleni",
                column: "PodjetjeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odtegljaji");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Pogodbe");

            migrationBuilder.DropTable(
                name: "PostavkePlacilneListe");

            migrationBuilder.DropTable(
                name: "PlacilneListe");

            migrationBuilder.DropTable(
                name: "Obracuni");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Podjetja");
        }
    }
}
