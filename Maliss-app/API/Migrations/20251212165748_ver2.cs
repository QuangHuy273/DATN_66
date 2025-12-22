using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnhDaTai",
                table: "monAns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonAnId",
                table: "anhs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_anhs_MonAnId",
                table: "anhs",
                column: "MonAnId");

            migrationBuilder.AddForeignKey(
                name: "FK_anhs_monAns_MonAnId",
                table: "anhs",
                column: "MonAnId",
                principalTable: "monAns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_anhs_monAns_MonAnId",
                table: "anhs");

            migrationBuilder.DropIndex(
                name: "IX_anhs_MonAnId",
                table: "anhs");

            migrationBuilder.DropColumn(
                name: "AnhDaTai",
                table: "monAns");

            migrationBuilder.DropColumn(
                name: "MonAnId",
                table: "anhs");
        }
    }
}
