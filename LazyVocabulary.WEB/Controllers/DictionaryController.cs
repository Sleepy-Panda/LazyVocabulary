using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    [Authorize]
    public class DictionaryController : Controller
    {
        private DictionaryService _dictionaryService;

        public DictionaryController(DictionaryService service)
        {
            _dictionaryService = service;
        }

        // GET: Dictionary
        [HttpGet]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = _dictionaryService.GetByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dto = resultWithData.ResultData;

            var model = dto
                .Select(d => new IndexDictionaryViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    SourceLanguageImagePath = d.SourceLanguageImagePath,
                    TargetLanguageImagePath = d.TargetLanguageImagePath,
                });

            return View(model);
        }

        // GET: Dictionary/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Dictionary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDictionaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            var dto = new DictionaryDTO
            {
                Name = model.Name,
                Description = model.Description,
                ApplicationUserId = User.Identity.GetUserId(),
                // TODO
                SourceLanguageId = 1,
                TargetLanguageId = 2,
            };

            var result = await _dictionaryService.Create(dto);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Create");
        }

        [HttpGet]
        public JsonResult IsDictionaryNameAvailable(string name)
        {
            bool result = _dictionaryService.IsDictionaryNameAvailable(name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Dictionary/Edit/1
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resultWithData = _dictionaryService.GetById(id.Value);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dto = resultWithData.ResultData;

            if (dto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var model = new EditDictionaryViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
            };

            return View("Edit", model);
        }

        // POST: Dictionary/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditDictionaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            var dto = new DictionaryDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ApplicationUserId = User.Identity.GetUserId(),
                // TODO
                SourceLanguageId = 1,
                TargetLanguageId = 2,
            };

            var result = await _dictionaryService.Update(dto);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Edit");
        }
    }
}