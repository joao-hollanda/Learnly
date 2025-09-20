using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnly.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Correto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acertou",
                table: "SimuladoQuestoes");

            migrationBuilder.AlterColumn<int>(
                name: "SimuladoId",
                table: "Simulados",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SimuladoId",
                table: "Simulados",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "Acertou",
                table: "SimuladoQuestoes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
