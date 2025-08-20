using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourChickenGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveToChicken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Chickens",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Chickens");
        }
    }
}
