using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourChickenGuide.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFlockManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chickens_FlockManagers_FlockManagerId",
                table: "Chickens");

            migrationBuilder.DropTable(
                name: "FlockManagers");

            migrationBuilder.DropIndex(
                name: "IX_Chickens_FlockManagerId",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "FlockManagerId",
                table: "Chickens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlockManagerId",
                table: "Chickens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FlockManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlockManagers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_FlockManagerId",
                table: "Chickens",
                column: "FlockManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chickens_FlockManagers_FlockManagerId",
                table: "Chickens",
                column: "FlockManagerId",
                principalTable: "FlockManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
