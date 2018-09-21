using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackAssessments.Data.Migrations
{
    public partial class MovedBarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Checkpoint");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "AcquiredItem",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "AcquiredItem");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Checkpoint",
                maxLength: 50,
                nullable: true);
        }
    }
}
