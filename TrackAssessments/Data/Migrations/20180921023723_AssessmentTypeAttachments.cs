using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackAssessments.Data.Migrations
{
    public partial class AssessmentTypeAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Attachment",
                table: "RequiredItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentContentType",
                table: "RequiredItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentFileName",
                table: "RequiredItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "RequiredItem");

            migrationBuilder.DropColumn(
                name: "AttachmentContentType",
                table: "RequiredItem");

            migrationBuilder.DropColumn(
                name: "AttachmentFileName",
                table: "RequiredItem");
        }
    }
}
