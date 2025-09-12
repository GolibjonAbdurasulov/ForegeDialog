using System;
using System.Collections.Generic;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    IsSigned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "news_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "our_partners",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    link = table.Column<string>(type: "text", nullable: true),
                    pictures_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_our_partners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "our_resources",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    description = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    file_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_our_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "our_valued_clients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_our_valued_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OurServices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    description = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    pictures_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServices", x => x.id);
                });

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
                name: "resourceCategory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resourceCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statistics",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    happy_clients = table.Column<int>(type: "jsonb", nullable: false),
                    projects = table.Column<int>(type: "jsonb", nullable: false),
                    team_members = table.Column<int>(type: "jsonb", nullable: false),
                    years_experience = table.Column<int>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statistics", x => x.id);
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
                name: "translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    uz = table.Column<string>(type: "text", nullable: true),
                    en = table.Column<string>(type: "text", nullable: true),
                    ru = table.Column<string>(type: "text", nullable: true),
                    ger = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    IsSigned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "views",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_views", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "our_categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    pictures_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_our_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_our_categories_file_model_pictures_id",
                        column: x => x.pictures_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "our_teams",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    role = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    experience = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    skills = table.Column<List<MultiLanguageField>>(type: "jsonb", nullable: true),
                    pictures_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_our_teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_our_teams_file_model_pictures_id",
                        column: x => x.pictures_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resource",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: true),
                    subject = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    published_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    size = table.Column<string>(type: "text", nullable: true),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource", x => x.id);
                    table.ForeignKey(
                        name: "FK_resource_file_model_file_id",
                        column: x => x.file_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_our_categories_pictures_id",
                table: "our_categories",
                column: "pictures_id");

            migrationBuilder.CreateIndex(
                name: "IX_our_teams_pictures_id",
                table: "our_teams",
                column: "pictures_id");

            migrationBuilder.CreateIndex(
                name: "IX_resource_file_id",
                table: "resource",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "IX_translations_code",
                table: "translations",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_models");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "news_category");

            migrationBuilder.DropTable(
                name: "our_categories");

            migrationBuilder.DropTable(
                name: "our_partners");

            migrationBuilder.DropTable(
                name: "our_resources");

            migrationBuilder.DropTable(
                name: "our_teams");

            migrationBuilder.DropTable(
                name: "our_valued_clients");

            migrationBuilder.DropTable(
                name: "OurServices");

            migrationBuilder.DropTable(
                name: "resource");

            migrationBuilder.DropTable(
                name: "resourceCategory");

            migrationBuilder.DropTable(
                name: "statistics");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "translations");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "views");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "file_model");
        }
    }
}
