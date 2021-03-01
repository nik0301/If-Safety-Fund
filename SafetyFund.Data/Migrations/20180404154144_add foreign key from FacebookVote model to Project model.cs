using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SafetyFund.Data.Migrations
{
    public partial class addforeignkeyfromFacebookVotemodeltoProjectmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FacebookVotes_project_id",
                table: "FacebookVotes",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacebookVotes_Projects_project_id",
                table: "FacebookVotes",
                column: "project_id",
                principalTable: "Projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacebookVotes_Projects_project_id",
                table: "FacebookVotes");

            migrationBuilder.DropIndex(
                name: "IX_FacebookVotes_project_id",
                table: "FacebookVotes");
        }
    }
}
