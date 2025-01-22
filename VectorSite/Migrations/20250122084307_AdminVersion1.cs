using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorSite.Migrations
{
    /// <inheritdoc />
    public partial class AdminVersion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Payments",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "Time");
        }
    }
}
