﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementSystem.Data;

#nullable disable

namespace TaskManagementSystem.Migrations
{
    [DbContext(typeof(TaskManagementDBContext))]
    partial class TaskManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaskManagementSystem.Entity.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("StatusId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskUser", b =>
                {
                    b.Property<int>("TasksId")
                        .HasColumnType("int");

                    b.Property<int>("TeamMembersId")
                        .HasColumnType("int");

                    b.HasKey("TasksId", "TeamMembersId");

                    b.HasIndex("TeamMembersId");

                    b.ToTable("TaskUser");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Attachment", b =>
                {
                    b.HasOne("TaskManagementSystem.Entity.Task", null)
                        .WithMany("Attachments")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Comment", b =>
                {
                    b.HasOne("TaskManagementSystem.Entity.Task", null)
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Task", b =>
                {
                    b.HasOne("TaskManagementSystem.Entity.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagementSystem.Entity.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.User", b =>
                {
                    b.HasOne("TaskManagementSystem.Entity.Department", null)
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("TaskUser", b =>
                {
                    b.HasOne("TaskManagementSystem.Entity.Task", null)
                        .WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagementSystem.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("TeamMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagementSystem.Entity.Task", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
