using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Restaurant.Migrations
{
    public partial class ManytoManyCommandeProduit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produit_Commande_CommandeId",
                table: "Produit");

            migrationBuilder.DropIndex(
                name: "IX_Produit_CommandeId",
                table: "Produit");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "Produit");

            migrationBuilder.CreateTable(
                name: "CommandeProduit",
                columns: table => new
                {
                    CommandeProduitsId = table.Column<int>(type: "int", nullable: false),
                    CommandesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeProduit", x => new { x.CommandeProduitsId, x.CommandesId });
                    table.ForeignKey(
                        name: "FK_CommandeProduit_Commande_CommandesId",
                        column: x => x.CommandesId,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeProduit_Produit_CommandeProduitsId",
                        column: x => x.CommandeProduitsId,
                        principalTable: "Produit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandeProduit_CommandesId",
                table: "CommandeProduit",
                column: "CommandesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandeProduit");

            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "Produit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produit_CommandeId",
                table: "Produit",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produit_Commande_CommandeId",
                table: "Produit",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id");
        }
    }
}
