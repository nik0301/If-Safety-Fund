using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IFLike.DAL.Migrations
{
    public partial class NewProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetIpLocation]') AND type in (N'P')) 
                DROP PROCEDURE [dbo].[GetIpLocation]");
            migrationBuilder.Sql(
                @"CREATE PROCEDURE [dbo].[GetIpLocation]
                    (
                       @Ip int
                    )
                    AS
                    BEGIN
                        SET NOCOUNT ON
                        SELECT country_code CountryCode, country_name CountryName , region_name RegionName, city_name CityName FROM dbo.ip2location WHERE (ip_from = (SELECT max(ip_from) FROM dbo.ip2location WHERE ip_from <= @Ip ) AND ip_to >= @Ip )
                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
