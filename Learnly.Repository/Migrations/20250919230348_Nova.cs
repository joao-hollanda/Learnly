using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnly.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Nova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    QuestaoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Disciplina = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    Lingua = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Contexto = table.Column<string>(type: "TEXT", nullable: true),
                    Arquivos = table.Column<string>(type: "TEXT", nullable: true),
                    IntroducaoAlternativa = table.Column<string>(type: "TEXT", nullable: true),
                    AlternativaCorreta = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.QuestaoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: true),
                    StatusConta = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlanoEstudoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    AlternativaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Letra = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Texto = table.Column<string>(type: "TEXT", nullable: false),
                    Arquivo = table.Column<string>(type: "TEXT", nullable: true),
                    Correta = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.AlternativaId);
                    table.ForeignKey(
                        name: "FK_Alternativas_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "QuestaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosEstudo",
                columns: table => new
                {
                    PlanoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Objetivo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HorasPorSemana = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusPlano = table.Column<bool>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosEstudo", x => x.PlanoId);
                    table.ForeignKey(
                        name: "FK_PlanosEstudo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simulados",
                columns: table => new
                {
                    SimuladoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NotaFinal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Desempenho_QuantidadeDeQuestoes = table.Column<int>(type: "INTEGER", nullable: true),
                    Desempenho_QuantidadeDeAcertos = table.Column<int>(type: "INTEGER", nullable: true),
                    Desempenho_Feedback = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulados", x => x.SimuladoId);
                    table.ForeignKey(
                        name: "FK_Simulados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasSimulado",
                columns: table => new
                {
                    RespostaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimuladoId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlternativaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasSimulado", x => x.RespostaId);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_Alternativas_AlternativaId",
                        column: x => x.AlternativaId,
                        principalTable: "Alternativas",
                        principalColumn: "AlternativaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "QuestaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_Simulados_SimuladoId",
                        column: x => x.SimuladoId,
                        principalTable: "Simulados",
                        principalColumn: "SimuladoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimuladoQuestoes",
                columns: table => new
                {
                    SimuladoQuestaoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimuladoId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Acertou = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimuladoQuestoes", x => x.SimuladoQuestaoId);
                    table.ForeignKey(
                        name: "FK_SimuladoQuestoes_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "QuestaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimuladoQuestoes_Simulados_SimuladoId",
                        column: x => x.SimuladoId,
                        principalTable: "Simulados",
                        principalColumn: "SimuladoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosEstudo_UsuarioId",
                table: "PlanosEstudo",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_AlternativaId",
                table: "RespostasSimulado",
                column: "AlternativaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_QuestaoId",
                table: "RespostasSimulado",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_SimuladoId",
                table: "RespostasSimulado",
                column: "SimuladoId");

            migrationBuilder.CreateIndex(
                name: "IX_SimuladoQuestoes_QuestaoId",
                table: "SimuladoQuestoes",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SimuladoQuestoes_SimuladoId",
                table: "SimuladoQuestoes",
                column: "SimuladoId");

            migrationBuilder.CreateIndex(
                name: "IX_Simulados_UsuarioId",
                table: "Simulados",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanosEstudo");

            migrationBuilder.DropTable(
                name: "RespostasSimulado");

            migrationBuilder.DropTable(
                name: "SimuladoQuestoes");

            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "Simulados");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
