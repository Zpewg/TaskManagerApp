﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task_Manager.Repository;

#nullable disable

namespace Task_Manager.Migrations
{
    [DbContext(typeof(MyAppDbContext))]
    partial class MyAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Task_Manager.Entities.TaskJournal", b =>
                {
                    b.Property<int>("idTaskJournal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idtask_journal");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idTaskJournal"));

                    b.Property<int>("idUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<string>("journalText")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("journal_text");

                    b.HasKey("idTaskJournal");

                    b.ToTable("task_journal");
                });

            modelBuilder.Entity("Task_Manager.Entities.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idUser"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("phone_number");

                    b.HasKey("idUser");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Task_Manager.Entities.UserTasks", b =>
                {
                    b.Property<int>("idUserTasks")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("iduser_tasks");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idUserTasks"));

                    b.Property<DateOnly>("date")
                        .HasColumnType("date")
                        .HasColumnName("date_of_task");

                    b.Property<int>("idUser")
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<string>("nameOfTask")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name_of_task");

                    b.Property<TimeOnly>("time")
                        .HasColumnType("time(6)")
                        .HasColumnName("time_of_task");

                    b.Property<string>("userTaskCol")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.HasKey("idUserTasks");

                    b.ToTable("user_tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
