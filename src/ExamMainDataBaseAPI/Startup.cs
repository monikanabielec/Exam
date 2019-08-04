﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamContract.MainDbModels;
using ExamMainDataBaseAPI.DAL;
using ExamMainDataBaseAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExamMainDataBaseAPI
{
    public class Startup
    {
        const string SQLite = "SQLite";
        const string SQL = "SQL";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            switch (Configuration.GetSection("UseDatabase").Value)
            {
                case SQLite:
                    services.AddDbContext<ExamQuestionsDbContext>(o => o.UseSqlite(Configuration.GetConnectionString("SQLiteConnection")));
                    break;
                case SQL:
                    services.AddDbContext<ExamQuestionsDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));
                    break;
            }
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(o => o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented);
            services.AddTransient<Repository<Answer>>();
            services.AddTransient<Repository<Question>>();           
            services.AddTransient<Repository<Exam>>();
            services.AddTransient<Repository<User>>();
            services.AddTransient<UnitOfWork>();       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
