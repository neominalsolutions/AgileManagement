using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileManagement.Persistence.EF.migrations.projectdb
{
    public partial class ProductBacklogItemPrivateTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductBacklogItemTask",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    ContributorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductBackLogItemId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBacklogItemTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBacklogItemTask_Contributor_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "Contributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBacklogItemTask_ProductBackLogItems_ProductBackLogItemId",
                        column: x => x.ProductBackLogItemId,
                        principalTable: "ProductBackLogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBacklogItemTask_ContributorId",
                table: "ProductBacklogItemTask",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBacklogItemTask_ProductBackLogItemId",
                table: "ProductBacklogItemTask",
                column: "ProductBackLogItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBacklogItemTask");
        }
    }
}
