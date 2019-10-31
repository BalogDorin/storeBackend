using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class EntityProdusPozeContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nume = table.Column<string>(nullable: true),
                    Prenume = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Poza",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdProdus = table.Column<Guid>(nullable: false),
                    CalePoza = table.Column<string>(nullable: true),
                    IsProduct = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Pret = table.Column<double>(nullable: false),
                    PretRedus = table.Column<double>(nullable: true),
                    Descriere = table.Column<string>(nullable: true),
                    Marime = table.Column<string>(nullable: true),
                    Titlu = table.Column<string>(nullable: true),
                    Culoare = table.Column<string>(nullable: true),
                    Categorie = table.Column<string>(nullable: true),
                    CaleFolder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Poza");

            migrationBuilder.DropTable(
                name: "Produs");
        }
    }
}
