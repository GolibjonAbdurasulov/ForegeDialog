using System.Collections.Generic;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class AddImageTablesBugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // image_category
            migrationBuilder.CreateTable(
                name: "image_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_category", x => x.id);
                });

            // image_model
            migrationBuilder.CreateTable(
                name: "image_model",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_id = table.Column<long>(type: "bigint", nullable: false),
                    image_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_model", x => x.id);
                });

            // pictures_models
            migrationBuilder.CreateTable(
                name: "pictures_models",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    images = table.Column<List<long>>(type: "bigint[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_pictures_models_image_category_category_id",
                        column: x => x.category_id,
                        principalTable: "image_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pictures_models_category_id",
                table: "pictures_models",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pictures_models");

            migrationBuilder.DropTable(
                name: "image_model");

            migrationBuilder.DropTable(
                name: "image_category");
        }
    
    }
}
