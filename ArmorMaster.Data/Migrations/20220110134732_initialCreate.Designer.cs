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
    [Migration("20220110134732_initialCreate")]
    partial class initialCreate
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

                    b.Property<double>("BaseStatQuantity")
                        .HasColumnType("float");

                    b.Property<string>("BaseStatType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemLevel")
                        .HasColumnType("int");

                    b.Property<int>("ItemPotential")
                        .HasColumnType("int");

                    b.Property<string>("ItemRarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemUpgradeLevel")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerID")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("PlayerID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.ItemBonusStat", b =>
                {
                    b.Property<int>("ItemBonusStatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<double>("StatQuantity")
                        .HasColumnType("float");

                    b.Property<string>("StatType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemBonusStatID");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemStats");
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

            modelBuilder.Entity("ArmorMaster.Data.Models.ItemBonusStat", b =>
                {
                    b.HasOne("ArmorMaster.Data.Models.Item", "Item")
                        .WithMany("ItemBonusStats")
                        .HasForeignKey("ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Item", b =>
                {
                    b.Navigation("ItemBonusStats");
                });

            modelBuilder.Entity("ArmorMaster.Data.Models.Player", b =>
                {
                    b.Navigation("EquipedItems");
                });
#pragma warning restore 612, 618
        }
    }
}