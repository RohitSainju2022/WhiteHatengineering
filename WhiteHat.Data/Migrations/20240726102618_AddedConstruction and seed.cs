using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteHat.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedConstructionandseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constructions",
                columns: table => new
                {
                    ConstructionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReadMore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions", x => x.ConstructionId);
                });

            migrationBuilder.InsertData(
                table: "Constructions",
                columns: new[] { "ConstructionId", "Description", "Image", "IsDeleted", "ReadMore", "Title" },
                values: new object[,]
                {
                    { 1, "I am Upvc Description check", "", false, "I am Upvc Readmore Check", "Upvc" },
                    { 2, "I am Upvc Description check 2", "", false, "I am Upvc Readmore Check 2", "Upvc2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constructions");
        }
    }
}
