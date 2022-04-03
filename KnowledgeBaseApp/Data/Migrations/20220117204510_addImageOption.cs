using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeBaseApp.Data.Migrations
{
    public partial class addImageOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ArticleImage",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleImage",
                table: "Articles");
        }
    }
}
