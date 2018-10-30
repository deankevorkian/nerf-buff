using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerfBuff.Migrations
{
    public partial class CreateBlogDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nb_users");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PostID = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 10, nullable: true),
                    Author = table.Column<string>(maxLength: 10, nullable: true),
                    Content = table.Column<string>(maxLength: 10, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 10, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    Author = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.CreateTable(
                name: "nb_users",
                columns: table => new
                {
                    nb_user_id = table.Column<int>(nullable: false),
                    nb_pass_hash = table.Column<string>(maxLength: 10, nullable: false),
                    nb_user_name = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nb_users", x => x.nb_user_id);
                });
        }
    }
}
