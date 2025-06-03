using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CamnbiosEnLaEmliminacionCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndicadoresPaises_MacroIndicadores_MacroIndicadorId",
                table: "IndicadoresPaises");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicadoresPaises_MacroIndicadores_MacroIndicadorId",
                table: "IndicadoresPaises",
                column: "MacroIndicadorId",
                principalTable: "MacroIndicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndicadoresPaises_MacroIndicadores_MacroIndicadorId",
                table: "IndicadoresPaises");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicadoresPaises_MacroIndicadores_MacroIndicadorId",
                table: "IndicadoresPaises",
                column: "MacroIndicadorId",
                principalTable: "MacroIndicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
