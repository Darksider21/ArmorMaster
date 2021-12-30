﻿// <auto-generated />
using System;
using ArmorMaster.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArmorMaster.Data.Migrations
{
    [DbContext(typeof(ArmorMasterContext))]
    [Migration("20211230123721_levels")]
    partial class levels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArmorMaster.Data.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("Potential")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("PlayerID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.ItemStat", b =>
                {
                    b.Property<int>("ItemStatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemStatTypeId")
                        .HasColumnType("int");

                    b.Property<double>("StatQuantity")
                        .HasColumnType("float");

                    b.HasKey("ItemStatID");

                    b.HasIndex("ItemId");

                    b.HasIndex("ItemStatTypeId");

                    b.ToTable("ItemStats");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.ItemStatType", b =>
                {
                    b.Property<int>("ItemStatTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemStatTypeId");

                    b.ToTable("ItemStatTypes");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Item", b =>
                {
                    b.HasOne("ArmorMaster.Data.Models.Player", "Player")
                        .WithMany("EquipedItems")
                        .HasForeignKey("PlayerID");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.ItemStat", b =>
                {
                    b.HasOne("ArmorMaster.Data.Models.Item", "Item")
                        .WithMany("ItemStats")
                        .HasForeignKey("ItemId");

                    b.HasOne("ArmorMaster.Data.Models.ItemStatType", "ItemStatType")
                        .WithMany()
                        .HasForeignKey("ItemStatTypeId");

                    b.Navigation("Item");

                    b.Navigation("ItemStatType");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Item", b =>
                {
                    b.Navigation("ItemStats");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Player", b =>
                {
                    b.Navigation("EquipedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
