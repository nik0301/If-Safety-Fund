using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SafetyFund.Data.Migrations
{
    public partial class refactorvotesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraugiemVotes");

            migrationBuilder.DropTable(
                name: "FacebookVotes");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    socialmedia_name = table.Column<string>(nullable: false),
                    user_id = table.Column<string>(nullable: false),
                    voting_datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Votes_Projects_project_id",
                        column: x => x.project_id,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_project_id",
                table: "Votes",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.CreateTable(
                name: "DraugiemVotes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: false),
                    voting_datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraugiemVotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_DraugiemVotes_Projects_project_id",
                        column: x => x.project_id,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacebookVotes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: false),
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
                name: "IX_DraugiemVotes_project_id",
                table: "DraugiemVotes",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_FacebookVotes_project_id",
                table: "FacebookVotes",
                column: "project_id");
        }
    }
}
