using System;
using System.Collections.Generic;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class addmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_models",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    text = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    categories = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    tags = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    images = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    reading_time = table.Column<string>(type: "text", nullable: true),
                    published_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    publisher_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_blog_models_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    text = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    categories = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    tags = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    reading_time = table.Column<string>(type: "text", nullable: true),
                    images = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    published_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    publisher_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.id);
                    table.ForeignKey(
                        name: "FK_news_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_models_publisher_id",
                table: "blog_models",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_news_publisher_id",
                table: "news",
                column: "publisher_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_models");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
