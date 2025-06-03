using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregueTablaSimulacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulacionMacroIndicadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacroIndicadorId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulacionMacroIndicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulacionMacroIndicadores_MacroIndicadores_MacroIndicadorId",
                        column: x => x.MacroIndicadorId,
                        principalTable: "MacroIndicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulacionMacroIndicadores_MacroIndicadorId",
                table: "SimulacionMacroIndicadores",
                column: "MacroIndicadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulacionMacroIndicadores");
        }
    }
}
