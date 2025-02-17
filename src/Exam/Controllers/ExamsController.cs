﻿using Exam.Data.UnitOfWork;
using Exam.Services;
using ExamContract.MainDbModels;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Controllers
{
    [AuthorizeByRoles(RoleEnum.admin, RoleEnum.teacher)]
    public class ExamsController : MyBaseController<ExamContract.MainDbModels.Exam>
    {
        private readonly Exams uow;
        private readonly ILogger logger;
        private object p2;

        public ExamsController(IStringLocalizer<SharedResource> localizer, WebApiClient<ExamContract.MainDbModels.Exam> service, Exams uow, ILogger<ExamsController> logger) : base(localizer, service)
        {
            this.uow = uow;
            this.logger = logger;
        }
        public override async Task<IActionResult> Index(int? parentId = null, int? questionId = null, string info = null, string warning = null, string error = null)
        {
            var currentRole = User.CurrentRoleEnum();
            var login = User.Identity.Name;
            switch (currentRole)
            {
                case RoleEnum.teacher:
                    ViewBag.Message = Localizer["Your exams"];
                    break;
                case RoleEnum.admin:
                    ViewBag.Message = Localizer["All exams"];
                    login = null;
                    break;
                default:
                    break;
            }
            ViewBag.Error = error;
            ViewBag.Warning = warning;
            ViewBag.Info = info;
            ViewBag.ExamId = parentId;
            ViewBag.QuestionId = questionId;
            bool onlyActive = Convert.ToBoolean(Request.Cookies[GlobalHelpers.ACTIVE]);
            ViewBag.OnlyActive = onlyActive;
            return View(await uow.GetList(login, onlyActive));
        }
        public new async Task<IActionResult> Delete(int? id, int? parentId = null)
        {
            string message = Localizer["Exams can not be removed, you can deactivate!"];
            logger.LogWarning(message);
            return await Task.Run<ActionResult>(() =>
            {
                return RedirectToAction(nameof(Index), new { error = message });
            });
        }
        public IActionResult SetActive(bool active)
        {
            logger.LogInformation($"Exams - SetActive {active}");
            Response.Cookies.Append(GlobalHelpers.ACTIVE, active.ToString(), new Microsoft.AspNetCore.Http.CookieOptions());
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Clone(int id)
        {
            await uow.Clone(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ExamResults(int id)
        {
            var (exam, examResults) = await uow.GetResultsForExam(id);
            ViewBag.Exam = exam;
            var model = examResults;            
            return View(model);
        }
    }
}