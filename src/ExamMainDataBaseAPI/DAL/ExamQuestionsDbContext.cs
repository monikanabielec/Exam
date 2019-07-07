﻿using System;
using ExamContract.MainDbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamMainDataBaseAPI.Models
{
    public partial class ExamQuestionsDbContext : DbContext
    {
     
        public ExamQuestionsDbContext(DbContextOptions<ExamQuestionsDbContext> options)
           : base(options)
        {
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answer { get; set; }      
        public DbSet<Question> Questions { get; set; }       
    }
}
