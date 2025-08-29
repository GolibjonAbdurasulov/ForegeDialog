using System;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class addresource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_resource_file_id",
                table: "resource",
                column: "file_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resource");

            migrationBuilder.DropTable(
                name: "resourceCategory");
        }
    }
}
