using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class create_products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups",
                column: "ParentId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_ParentId",
                table: "Groups",
                column: "ParentId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
