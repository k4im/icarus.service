using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icarus.jwtManager.Migrations
{
    /// <inheritdoc />
    public partial class adequadoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "RefreshTokens",
                newName: "ChaveDeAcesso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChaveDeAcesso",
                table: "RefreshTokens",
                newName: "UserEmail");
        }
    }
}
