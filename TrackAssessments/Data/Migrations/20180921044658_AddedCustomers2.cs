using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackAssessments.Data.Migrations
{
    public partial class AddedCustomers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Customer",
                nullable: true);
        }
    }
}
