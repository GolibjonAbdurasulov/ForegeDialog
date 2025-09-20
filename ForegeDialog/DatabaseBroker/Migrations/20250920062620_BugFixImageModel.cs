using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class BugFixImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eski bigint ustunni olib tashlaymiz
            migrationBuilder.DropColumn(
                name: "file_id",
                table: "image_model");

            // Yangi uuid ustunini qo'shamiz
            migrationBuilder.AddColumn<Guid>(
                name: "file_id",
                table: "image_model",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // uuid ustunni olib tashlaymiz
            migrationBuilder.DropColumn(
                name: "file_id",
                table: "image_model");

            // Orqaga qaytishda bigint ustunni tiklaymiz
            migrationBuilder.AddColumn<long>(
                name: "file_id",
                table: "image_model",
                type: "bigint",
                nullable: false);
        }
    }
}
