using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace postService.Migrations
{
    public partial class postServiceDatapostServiceContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    PostTime = table.Column<DateTime>(nullable: false),
                    Post_price = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
