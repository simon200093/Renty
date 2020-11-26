using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace offerService.Migrations
{
    public partial class offerServiceDataofferServiceContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferTime = table.Column<DateTime>(nullable: false),
                    OfferPrice = table.Column<string>(nullable: true),
                    OfferStatus = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    PostId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");
        }
    }
}
