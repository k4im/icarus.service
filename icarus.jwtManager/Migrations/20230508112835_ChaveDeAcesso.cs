using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icarus.jwtManager.Migrations
{
    /// <inheritdoc />
    public partial class ChaveDeAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChaveDeAcesso",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChaveDeAcesso",
                table: "AspNetUsers");
        }
    }
}
