using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImageEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PictureDate",
                table: "Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Images",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PictureDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Resolution",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
