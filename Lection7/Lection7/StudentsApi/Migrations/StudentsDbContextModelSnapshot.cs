﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentsApi;

namespace StudentsApi.Migrations
{
    [DbContext(typeof(StudentsDbContext))]
    partial class StudentsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("StudentsApi.Entities.StudentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentsApi.Entities.StudentTeacherEntity", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<string>("TeacherName")
                        .HasColumnType("text");

                    b.HasKey("StudentId", "TeacherName");

                    b.HasIndex("TeacherName");

                    b.ToTable("StudentTeacher");
                });

            modelBuilder.Entity("StudentsApi.Entities.TeacherEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Discipline")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("StudentsApi.Entities.StudentTeacherEntity", b =>
                {
                    b.HasOne("StudentsApi.Entities.StudentEntity", "Student")
                        .WithMany("Teachers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsApi.Entities.TeacherEntity", "Teacher")
                        .WithMany("Students")
                        .HasForeignKey("TeacherName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
