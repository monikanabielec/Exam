﻿// <auto-generated />
using System;
using ExamMainDataBaseAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamMainDataBaseAPI.Migrations
{
    [DbContext(typeof(ExamQuestionsDbContext))]
    partial class ExamQuestionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExamContract.MainDbModels.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.AnswersType", b =>
                {
                    b.Property<string>("AnswerType")
                        .ValueGeneratedOnAdd();

                    b.HasKey("AnswerType");

                    b.ToTable("AnswersType");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DurationMinutes");

                    b.Property<string>("Login");

                    b.Property<decimal>("MaxPoints");

                    b.Property<DateTime>("MaxStart");

                    b.Property<DateTime>("MinStart");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerType");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.QuestionAnswer", b =>
                {
                    b.Property<int>("AnswerId");

                    b.Property<int>("QuestionId");

                    b.HasKey("AnswerId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswer");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.User", b =>
                {
                    b.Property<string>("Login")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Login");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExamContract.MainDbModels.QuestionAnswer", b =>
                {
                    b.HasOne("ExamContract.MainDbModels.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExamContract.MainDbModels.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
