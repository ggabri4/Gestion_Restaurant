using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Restaurant.Migrations
{
    public partial class minorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "Table");

            migrationBuilder.AlterColumn<double>(
                name: "Montant",
                table: "Paiement",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "FactureAPayerId",
                table: "Paiement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement",
                column: "FactureAPayerId",
                principalTable: "Facture",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement");

            migrationBuilder.AddColumn<bool>(
                name: "Etat",
                table: "Table",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Montant",
                table: "Paiement",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FactureAPayerId",
                table: "Paiement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Facture_FactureAPayerId",
                table: "Paiement",
                column: "FactureAPayerId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
