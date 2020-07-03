using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Statistics.Data.Migrations
{
    public partial class InitialStatisticsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookView",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewView",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBooks = table.Column<int>(nullable: false),
                    TotalReviews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookView_BookId",
                table: "BookView",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewView_ReviewId",
                table: "ReviewView",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookView");

            migrationBuilder.DropTable(
                name: "ReviewView");

            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
