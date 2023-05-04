using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icarus.projetos.Migrations
{
    /// <inheritdoc />
    public partial class CamposAtuaolizados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projetos",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Projetos",
                newName: "Name");
        }
    }
}
