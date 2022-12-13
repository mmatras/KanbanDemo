﻿// <auto-generated />
using System;
using DemoKanban.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoKanban.Migrations
{
    [DbContext(typeof(KanbanContext))]
    partial class KanbanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DemoKanban.Models.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuditLog");
                });

            modelBuilder.Entity("DemoKanban.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AssignedToId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUrgent")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedToId = 1,
                            Deadline = new DateTime(2023, 3, 13, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9756),
                            IsUrgent = true,
                            Notes = "Ten temat musi być bardzo dobrze opanowany",
                            State = 0,
                            Title = "Nauczyć się C# oraz .NET"
                        },
                        new
                        {
                            Id = 2,
                            AssignedToId = 3,
                            Deadline = new DateTime(2022, 12, 16, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9790),
                            IsUrgent = false,
                            Notes = "Rwównież Razor Pages",
                            State = 1,
                            Title = "Nauczyć się ASP.NET MVC"
                        },
                        new
                        {
                            Id = 3,
                            Deadline = new DateTime(2023, 2, 1, 22, 33, 54, 299, DateTimeKind.Local).AddTicks(9793),
                            IsUrgent = false,
                            Notes = "Mamy dwa projekty do wyboru lub można wybrać swój",
                            State = 0,
                            Title = "Zrobić samodzielny proejkt"
                        },
                        new
                        {
                            Id = 4,
                            IsUrgent = false,
                            Notes = "Język SQL jest językiem deklaratywnym",
                            State = 2,
                            Title = "Nauczyć się RDBMS"
                        });
                });

            modelBuilder.Entity("DemoKanban.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayName = "matras",
                            Name = "Daniel",
                            Surname = "Matras"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayName = "",
                            Name = "Marcin",
                            Surname = "Nowak"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayName = "opolski",
                            Name = "Jan",
                            Surname = "Opolski"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DisplayName = "jdąb",
                            Name = "Magdalena",
                            Surname = "Dąbrowska"
                        });
                });

            modelBuilder.Entity("DemoKanban.Models.Issue", b =>
                {
                    b.HasOne("DemoKanban.Models.Person", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.Navigation("AssignedTo");
                });
#pragma warning restore 612, 618
        }
    }
}
