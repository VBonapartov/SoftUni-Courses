using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Reviews.Data.Migrations
{
    public partial class InitialReviewsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                    Author = table.Column<string>(maxLength: 100, nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    BookName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
