using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lnksnp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkClicks",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    clickDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    linkId = table.Column<int>(type: "INTEGER", nullable: false),
                    IP = table.Column<string>(type: "TEXT", nullable: true),
                    referer = table.Column<string>(type: "TEXT", nullable: true),
                    userAgent = table.Column<string>(type: "TEXT", nullable: true),
                    acceptLanguage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkClicks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LinkSnips",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortLink = table.Column<string>(type: "TEXT", nullable: false),
                    LongLink = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkSnips", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkClicks");

            migrationBuilder.DropTable(
                name: "LinkSnips");
        }
    }
}
