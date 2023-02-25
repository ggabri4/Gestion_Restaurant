using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Restaurant.Data.Migrations
{
    public partial class RelationOneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Montant",
                table: "Paiement");

            migrationBuilder.AddColumn<int>(
                name: "CommandeRattacheID",
                table: "Table",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommandeEtablitID",
                table: "Serveur",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactureAPayerID",
                table: "Paiement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FactureAPayerId",
                table: "Paiement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "Commande",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactureRattacherID",
                table: "Commande",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FactureRattacherId",
                table: "Commande",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CommandeEnChargeID",
                table: "Barman",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrepareCommandeId",
                table: "Barman",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Table_CommandeRattacheID",
                table: "Table",
                column: "CommandeRattacheID");

            migrationBuilder.CreateIndex(
                name: "IX_Serveur_CommandeEtablitID",
                table: "Serveur",
                column: "CommandeEtablitID");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_FactureAPayerId",
                table: "Paiement",
                column: "FactureAPayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_CommandeId",
                table: "Commande",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_FactureRattacherId",
                table: "Commande",
                column: "FactureRattacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Barman_PrepareCommandeId",
                table: "Barman",
                column: "PrepareCommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeId",
                table: "Barman",
                column: "PrepareCommandeId",
                principalTable: "Commande",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Commande_CommandeId",
                table: "Commande",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Facture_FactureRattacherId",
                table: "Commande",
                column: "FactureRattacherId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement",
                column: "FactureAPayerId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Serveur_Commande_CommandeEtablitID",
                table: "Serveur",
                column: "CommandeEtablitID",
                principalTable: "Commande",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Commande_CommandeRattacheID",
                table: "Table",
                column: "CommandeRattacheID",
                principalTable: "Commande",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeId",
                table: "Barman");

            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Commande_CommandeId",
                table: "Commande");

            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Facture_FactureRattacherId",
                table: "Commande");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement");

            migrationBuilder.DropForeignKey(
                name: "FK_Serveur_Commande_CommandeEtablitID",
                table: "Serveur");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Commande_CommandeRattacheID",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_CommandeRattacheID",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Serveur_CommandeEtablitID",
                table: "Serveur");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_FactureAPayerId",
                table: "Paiement");

            migrationBuilder.DropIndex(
                name: "IX_Commande_CommandeId",
                table: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_Commande_FactureRattacherId",
                table: "Commande");

            migrationBuilder.DropIndex(
                name: "IX_Barman_PrepareCommandeId",
                table: "Barman");

            migrationBuilder.DropColumn(
                name: "CommandeRattacheID",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "CommandeEtablitID",
                table: "Serveur");

            migrationBuilder.DropColumn(
                name: "FactureAPayerID",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "FactureAPayerId",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "FactureRattacherID",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "FactureRattacherId",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "CommandeEnChargeID",
                table: "Barman");

            migrationBuilder.DropColumn(
                name: "PrepareCommandeId",
                table: "Barman");

            migrationBuilder.AddColumn<double>(
                name: "Montant",
                table: "Paiement",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
