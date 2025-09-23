using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "reference_models",
                newName: "pictures_model_id");

            migrationBuilder.CreateTable(
                name: "reference_to_blog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    blog_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reference_to_blog", x => x.id);
                    table.ForeignKey(
                        name: "FK_reference_to_blog_our_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "our_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reference_to_blog_category_id",
                table: "reference_to_blog",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reference_to_blog");

            migrationBuilder.RenameColumn(
                name: "pictures_model_id",
                table: "reference_models",
                newName: "item_id");
        }
    }
}
