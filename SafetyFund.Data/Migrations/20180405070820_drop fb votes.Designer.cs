﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SafetyFund.Data;
using System;

namespace SafetyFund.Data.Migrations
{
    [DbContext(typeof(SafetyFundDbContext))]
    [Migration("20180405070820_drop fb votes")]
    partial class dropfbvotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SafetyFund.Data.Models.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnName("end_datetime");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnName("start_datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("SafetyFund.Data.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("CampaignId")
                        .HasColumnName("campaign_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description");

                    b.Property<byte[]>("Image")
                        .HasColumnName("image");

                    b.Property<string>("Intro")
                        .IsRequired()
                        .HasColumnName("intro");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SafetyFund.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnName("full_name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SafetyFund.Data.Models.Project", b =>
                {
                    b.HasOne("SafetyFund.Data.Models.Campaign", "Campaign")
                        .WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
