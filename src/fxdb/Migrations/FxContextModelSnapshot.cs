﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using fxdb;

namespace fxdb.Migrations
{
    [DbContext(typeof(FxContext))]
    partial class FxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("Relational:Sequence:.EffectIds", "'EffectIds', '', '1', '1', '', '', 'Int32', 'False'");

            modelBuilder.Entity("fxdb.Models.EffectItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR EffectIds");

                    b.Property<string>("name");

                    b.Property<string>("path");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("EffectItems");
                });
        }
    }
}
