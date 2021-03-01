using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SafetyFund.Data.Migrations
{
    public partial class dropfbvotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacebookVotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    table.ForeignKey(
                        name: "FK_FacebookVotes_Projects_project_id",
                        column: x => x.project_id,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookVotes_project_id",
                table: "FacebookVotes",
                column: "project_id");
        }
    }
}
