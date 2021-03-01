using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SafetyFund.Data.Migrations
{
    public partial class facebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacebookVotes",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    voting_datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookVotes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacebookVotes");
        }
    }
}
