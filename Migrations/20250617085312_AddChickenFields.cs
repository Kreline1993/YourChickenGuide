using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourChickenGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddChickenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Legband_Id",
                table: "Chickens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Chickens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Chickens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateOnly>(
                name: "HatchDate",
                table: "Chickens",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Chickens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Chickens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "HatchDate",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Chickens");

            migrationBuilder.UpdateData(
                table: "Chickens",
                keyColumn: "Legband_Id",
                keyValue: null,
                column: "Legband_Id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Legband_Id",
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
