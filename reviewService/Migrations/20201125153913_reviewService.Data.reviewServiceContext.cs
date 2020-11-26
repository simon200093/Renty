using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reviewService.Migrations
{
    public partial class reviewServiceDatareviewServiceContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewGrade = table.Column<int>(nullable: false),
                    ReviewContent = table.Column<string>(nullable: true),
                    ReviewTime = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    OfferId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
