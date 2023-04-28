using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icarus.projetos.Migrations
{
    /// <inheritdoc />
    public partial class Chapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chapa",
                table: "Projetos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chapa",
                table: "Projetos");
        }
    }
}
