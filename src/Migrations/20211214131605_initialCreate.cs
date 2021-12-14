using Microsoft.EntityFrameworkCore.Migrations;

namespace Book_Store.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    GenreId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Michelle", "Alexander" },
                    { 26, "Seth", "Grahame-Smith" },
                    { 25, "JK", "Rowling" },
                    { 23, "Augusten", "Burroughs" },
                    { 22, "Sun", "Tzu" },
                    { 21, "Mary", "Shelley" },
                    { 20, "George", "Orwell" },
                    { 19, "Toni", "Morrison" },
                    { 18, "David", "McCullough" },
                    { 17, "Stieg", "Larsson" },
                    { 16, "Aldous", "Huxley" },
                    { 14, "Dashiel", "Hammett" },
                    { 15, "Frank", "Herbert" },
                    { 12, "Tina", "Fey" },
                    { 2, "Stephen E.", "Ambrose" },
                    { 3, "Margaret", "Atwood" },
                    { 4, "Jane", "Austen" },
                    { 13, "Roxane", "Gay" },
                    { 6, "Emily", "Bronte" },
                    { 5, "James", "Baldwin" },
                    { 8, "Ta-Nehisi", "Coates" },
                    { 9, "Jared", "Diamond" },
                    { 10, "Joan", "Didion" },
                    { 11, "Daphne", "Du Maurier" },
                    { 7, "Agatha", "Christie" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { "scifi", "Science Finction" },
                    { "novel", "Novel" },
                    { "memoir", "Memoir" },
                    { "mystery", "Mystery" },
                    { "history", "History" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "GenreId", "Price", "Title" },
                values: new object[,]
                {
                    { 5, "novel", 10.99, "Beloved" },
                    { 15, "history", 15.5, "Guns, Germs, and Steel" },
                    { 9, "history", 15.0, "D-Day" },
                    { 4, "history", 11.5, "Band of Brothers" },
                    { 1, "history", 18.0, "1776" },
                    { 22, "scifi", 12.5, "The Handmaid's Tale" },
                    { 13, "scifi", 6.5, "Frankenstein" },
                    { 11, "scifi", 8.75, "Dune" },
                    { 8, "scifi", 16.25, "Brave New World" },
                    { 2, "scifi", 5.5, "1984" },
                    { 23, "mystery", 10.99, "The Maltese Falcon" },
                    { 21, "mystery", 8.5, "The Girl with the Dragon Tattoo" },
                    { 19, "mystery", 10.99, "Rebecca" },
                    { 20, "history", 5.75, "The Art of War" },
                    { 17, "mystery", 6.75, "Murder on the Orient Express" },
                    { 27, "memoir", 11.0, "Running With Scissors" },
                    { 25, "memoir", 13.5, "The Year of Magical Thinking" },
                    { 16, "memoir", 14.5, "Hunger" },
                    { 10, "memoir", 12.5, "Down and Out in Paris and London" },
                    { 7, "memoir", 4.25, "Bossypants" },
                    { 6, "memoir", 13.5, "Between the World and Me" },
                    { 29, "novel", 9.75, "Harry Potter and the Sorcerer's Stone" },
                    { 28, "novel", 8.75, "Pride and Prejudice and Zombies" },
                    { 26, "novel", 9.0, "Wuthering Heights" },
                    { 18, "novel", 8.5, "Pride and Prejudice" },
                    { 14, "novel", 10.25, "Go Tell it on the Mountain" },
                    { 12, "novel", 9.0, "Emma" },
                    { 3, "mystery", 4.5, "And Then There Were None" },
                    { 24, "history", 13.75, "The New Jim Crow" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 19, 5 },
                    { 9, 15 },
                    { 2, 9 },
                    { 2, 4 },
                    { 18, 1 },
                    { 3, 22 },
                    { 21, 13 },
                    { 15, 11 },
                    { 16, 8 },
                    { 20, 2 },
                    { 14, 23 },
                    { 17, 21 },
                    { 11, 19 },
                    { 7, 17 },
                    { 7, 3 },
                    { 23, 27 },
                    { 10, 25 },
                    { 13, 16 },
                    { 20, 10 },
                    { 12, 7 },
                    { 8, 6 },
                    { 25, 29 },
                    { 26, 28 },
                    { 4, 28 },
                    { 6, 26 },
                    { 4, 18 },
                    { 5, 14 },
                    { 4, 12 },
                    { 22, 20 },
                    { 1, 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
