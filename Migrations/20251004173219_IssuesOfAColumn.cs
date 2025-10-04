using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanBackend.Migrations
{
    /// <inheritdoc />
    public partial class IssuesOfAColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColumnId",
                table: "Issues",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_BoardId",
                table: "Issues",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ColumnId",
                table: "Issues",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Boards_BoardId",
                table: "Issues",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Boards_BoardId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Columns_ColumnId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_BoardId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ColumnId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "Issues");
        }
    }
}
