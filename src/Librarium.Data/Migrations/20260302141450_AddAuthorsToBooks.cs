using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarium.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorsToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BooksId",
                table: "BookAuthors",
                column: "BooksId");

            // This here is to create a default unknon author to the books that already exist in the database
            migrationBuilder.Sql("""
                INSERT INTO "Authors" 
                ("Id","FirstName","LastName")
                VALUES ('00000000-0000-0000-0000-000000000001', 'Unknown', 'Author');
                """);


            // And then we can link all the existing books to the unknown author
            migrationBuilder.Sql("""
                INSERT INTO "BookAuthors"
                ("AuthorsId","BooksId")
                SELECT '00000000-0000-0000-0000-000000000001', "Id"
                FROM "Books";
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");
        }
    }
}
