using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Restaurant.Migrations
{
    public partial class fixCommande : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Commande_CommandeId",
                table: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_Commande_CommandeId",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "NumeroCommande",
                table: "Commande");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "Commande",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCommande",
                table: "Commande",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_CommandeId",
                table: "Commande",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Commande_CommandeId",
                table: "Commande",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id");
        }
    }
}
