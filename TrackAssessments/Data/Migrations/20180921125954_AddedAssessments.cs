using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackAssessments.Data.Migrations
{
    public partial class AddedAssessments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentName = table.Column<string>(maxLength: 50, nullable: false),
                    DestinationID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    AssessmentTypeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assessment_AssessmentType_AssessmentTypeID",
                        column: x => x.AssessmentTypeID,
                        principalTable: "AssessmentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Destination_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Destination",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcquiredItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentID = table.Column<int>(nullable: false),
                    Acquired = table.Column<bool>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 50, nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Attachment = table.Column<byte[]>(nullable: true),
                    AttachmentContentType = table.Column<string>(nullable: true),
                    AttachmentFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquiredItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AcquiredItem_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcquiredItem_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcquiredItemID = table.Column<int>(nullable: false),
                    Barcode = table.Column<string>(maxLength: 50, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Checkpoint_AcquiredItem_AcquiredItemID",
                        column: x => x.AcquiredItemID,
                        principalTable: "AcquiredItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcquiredItem_AssessmentID",
                table: "AcquiredItem",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AcquiredItem_ItemTypeID",
                table: "AcquiredItem",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AssessmentTypeID",
                table: "Assessment",
                column: "AssessmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CustomerID",
                table: "Assessment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_DestinationID",
                table: "Assessment",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoint_AcquiredItemID",
                table: "Checkpoint",
                column: "AcquiredItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkpoint");

            migrationBuilder.DropTable(
                name: "AcquiredItem");

            migrationBuilder.DropTable(
                name: "Assessment");
        }
    }
}
