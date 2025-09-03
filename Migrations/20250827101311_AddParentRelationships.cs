using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourChickenGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddParentRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "father_Id",
                table: "Chickens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mother_Id",
                table: "Chickens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_father_Id",
                table: "Chickens",
                column: "father_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_mother_Id",
                table: "Chickens",
                column: "mother_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chickens_Chickens_father_Id",
                table: "Chickens",
                column: "father_Id",
                principalTable: "Chickens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chickens_Chickens_mother_Id",
                table: "Chickens",
                column: "mother_Id",
                principalTable: "Chickens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chickens_Chickens_father_Id",
                table: "Chickens");

            migrationBuilder.DropForeignKey(
                name: "FK_Chickens_Chickens_mother_Id",
                table: "Chickens");

            migrationBuilder.DropIndex(
                name: "IX_Chickens_father_Id",
                table: "Chickens");

            migrationBuilder.DropIndex(
                name: "IX_Chickens_mother_Id",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "father_Id",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "mother_Id",
                table: "Chickens");
        }
    }
}
