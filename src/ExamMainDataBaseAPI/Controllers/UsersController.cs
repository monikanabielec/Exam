﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamMainDataBaseAPI.Models;
using ExamMainDataBaseAPI.DAL;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ExamContract.MainDbModels;

namespace ExamMainDataBaseAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : MyBaseController<User>
    {
        private readonly Repository<User> repo;

        public UsersController(Repository<User> repo) : base(repo)
        {
            this.repo = repo;
        }
        [HttpGet("{examId}")]        
        public virtual async Task<ActionResult<IEnumerable<User>>> GetUsersForExam(int examId)
        {
            return await repo.FindBy(a => a.ExamId == examId && a.Active == true).ToListAsync();
        }
    }
}