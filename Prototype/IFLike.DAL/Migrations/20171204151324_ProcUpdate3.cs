﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IFLike.DAL.Migrations
{
    public partial class ProcUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                    @"ALTER PROCEDURE [dbo].[GetIpLocation]
                    (
                       @Ip int
                    )
                    AS
                    BEGIN
                        SET NOCOUNT ON
                        SELECT id, [IpFrom], [IpTo], countrycode CountryCode, countryname CountryName , regionname RegionName, cityname CityName FROM dbo.IpLocations WHERE (ipfrom = (SELECT max(ipfrom) FROM dbo.IpLocations WHERE ipfrom <= @Ip ) AND ipto >= @Ip )
                    END");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
