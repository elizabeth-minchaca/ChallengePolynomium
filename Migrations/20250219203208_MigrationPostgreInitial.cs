using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChallengePolynomius.Migrations
{
    /// <inheritdoc />
    public partial class MigrationPostgreInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "J.R.R. Tolkien" },
                    { 3, "George Orwell" },
                    { 4, "Isaac Asimov" },
                    { 5, "Agatha Christie" },
                    { 6, "Stephen King" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasía" },
                    { 2, "Ciencia Ficción" },
                    { 3, "Novela Gráfica" },
                    { 4, "Suspenso" },
                    { 5, "Misterio" },
                    { 6, "Terror" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "Harry Potter y la Piedra Filosofal" },
                    { 2, 1, 1, "Harry Potter y la Cámara Secreta" },
                    { 3, 1, 1, "Harry Potter y el Prisionero de Azkaban" },
                    { 4, 2, 1, "El Señor de los Anillos: La Comunidad del Anillo" },
                    { 5, 2, 1, "El Señor de los Anillos: Las Dos Torres" },
                    { 6, 2, 1, "El Señor de los Anillos: El Retorno del Rey" },
                    { 7, 3, 2, "1984" },
                    { 8, 3, 2, "Rebelión en la Granja" },
                    { 9, 4, 2, "Fundación" },
                    { 10, 4, 2, "Yo, Robot" },
                    { 11, 5, 5, "Asesinato en el Orient Express" },
                    { 12, 5, 5, "Diez Negritos" },
                    { 13, 6, 6, "It" },
                    { 14, 6, 6, "El Resplandor" },
                    { 15, 6, 6, "Cementerio de Animales" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
