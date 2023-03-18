using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Restaurant.Migrations
{
    public partial class FixConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeId",
                table: "Barman");

            migrationBuilder.DropColumn(
                name: "CommandeEnChargeID",
                table: "Barman");

            migrationBuilder.RenameColumn(
                name: "PrepareCommandeId",
                table: "Barman",
                newName: "PrepareCommandeID");

            migrationBuilder.RenameIndex(
                name: "IX_Barman_PrepareCommandeId",
                table: "Barman",
                newName: "IX_Barman_PrepareCommandeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeID",
                table: "Barman",
                column: "PrepareCommandeID",
                principalTable: "Commande",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeID",
                table: "Barman");

            migrationBuilder.RenameColumn(
                name: "PrepareCommandeID",
                table: "Barman",
                newName: "PrepareCommandeId");

            migrationBuilder.RenameIndex(
                name: "IX_Barman_PrepareCommandeID",
                table: "Barman",
                newName: "IX_Barman_PrepareCommandeId");

            migrationBuilder.AddColumn<int>(
                name: "CommandeEnChargeID",
                table: "Barman",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Barman_Commande_PrepareCommandeId",
                table: "Barman",
                column: "PrepareCommandeId",
                principalTable: "Commande",
                principalColumn: "Id");
        }
    }
}
