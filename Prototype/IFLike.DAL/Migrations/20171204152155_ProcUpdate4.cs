using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IFLike.DAL.Migrations
{
    public partial class ProcUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                    @"ALTER PROCEDURE [dbo].[GetIpLocation]
                   (
                      @Ip bigint
                   )
                   AS
                   BEGIN
                       SET NOCOUNT ON
                       SELECT * FROM IpLocations WHERE (IpFrom = (SELECT max(IpFrom) FROM IpLocations WHERE IpFrom <= @Ip ) AND IpTo >= @Ip )
                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
