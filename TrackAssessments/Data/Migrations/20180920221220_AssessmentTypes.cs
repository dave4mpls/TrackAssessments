using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackAssessments.Data.Migrations
{
    public partial class AssessmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RequiredItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentTypeID = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 50, nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequiredItem_AssessmentType_AssessmentTypeID",
                        column: x => x.AssessmentTypeID,
                        principalTable: "AssessmentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredItem_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_AssessmentTypeID",
                table: "RequiredItem",
                column: "AssessmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_ItemTypeID",
                table: "RequiredItem",
                column: "ItemTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequiredItem");

            migrationBuilder.DropTable(
                name: "AssessmentType");

            migrationBuilder.DropTable(
                name: "ItemType");
        }
    }
}
