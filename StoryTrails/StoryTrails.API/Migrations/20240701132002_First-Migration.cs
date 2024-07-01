using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryTrails.API.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BookName = table.Column<string>(type: "text", nullable: false),
                    Collection = table.Column<string>(type: "text", nullable: false),
                    User = table.Column<string>(type: "text", nullable: false),
                    PagesAmount = table.Column<int>(type: "integer", nullable: false),
                    Concluded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CollectionName = table.Column<string>(type: "text", nullable: false),
                    CollectionObjective = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    UserLogin = table.Column<string>(type: "text", nullable: false),
                    UserPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
