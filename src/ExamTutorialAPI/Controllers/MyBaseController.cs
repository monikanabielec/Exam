﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExamContract;
using ExamTutorialsAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamTutorialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MyBaseController<T> : ControllerBase where T : Entity
    {
        private readonly Repository<T> repo;

        public MyBaseController(Repository<T> repository)
        {
            repo = repository;
        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> Get(string login, bool? onlyActive = null)
        {
            Expression<Func<T, bool>> predicate;
            if (onlyActive == true && login == null)
                predicate = a => a.Active == true;
            else if (onlyActive != true && login != null)
                predicate = a => a.Login == login;
            else if (onlyActive == true && login != null)
                predicate = a => a.Active == true && a.Login == login;
            else
                return await repo.GetListAsync();
            return await repo.FindBy(predicate).ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> Get(int id)
        {
            var item = await repo.GetAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, T item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await repo.UpdateAsync(item);
            try
            {
                await repo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repo.FindBy(a => a.Id == id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T item)
        {
            await repo.AddAsync(item);
            await repo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(int id)
        {
            var item = await repo.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await repo.DeleteAsync(item.Id);
            await repo.SaveChangesAsync();
            return item;
        }
    }
}