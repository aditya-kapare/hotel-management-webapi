using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddGovtIdtoCabDrivers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GovernmentId",
                table: "CabDrivers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Unique_GovernmentId",
                table: "CabDrivers",
                column: "GovernmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unique_GovernmentId",
                table: "CabDrivers");

            migrationBuilder.DropColumn(
                name: "GovernmentId",
                table: "CabDrivers");
        }
    }
}
