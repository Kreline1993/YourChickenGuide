using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourChickenGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddFlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Chickens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Chickens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Chickens",
                keyColumn: "Notes",
                keyValue: null,
                column: "Notes",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Chickens",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Chickens",
                keyColumn: "Color",
                keyValue: null,
                column: "Color",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Chickens",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
