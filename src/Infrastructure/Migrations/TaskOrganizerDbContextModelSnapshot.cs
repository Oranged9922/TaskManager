﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TaskOrganizerDbContext))]
    partial class TaskOrganizerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-rc.2.23480.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Domain.TOTaskAggregate.TOTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssignedToId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskBlock", b =>
                {
                    b.Property<Guid>("BlockedById")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BlocksId")
                        .HasColumnType("TEXT");

                    b.HasKey("BlockedById", "BlocksId");

                    b.HasIndex("BlocksId");

                    b.ToTable("TaskBlock");
                });

            modelBuilder.Entity("Domain.TOTaskAggregate.TOTask", b =>
                {
                    b.HasOne("Domain.UserAggregate.User", "AssignedTo")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedToId");

                    b.HasOne("Domain.UserAggregate.User", "Creator")
                        .WithMany("CreatedTasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TaskBlock", b =>
                {
                    b.HasOne("Domain.TOTaskAggregate.TOTask", null)
                        .WithMany()
                        .HasForeignKey("BlockedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TOTaskAggregate.TOTask", null)
                        .WithMany()
                        .HasForeignKey("BlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("CreatedTasks");
                });
#pragma warning restore 612, 618
        }
    }
}