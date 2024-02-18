using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DUPL_Task_Project.Models;
using X.PagedList;

namespace DUPL_Task_Project.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookDbContext _db = new BookDbContext();

        // GET: Authors
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var authors = _db.Authors.OrderBy(a => a.AuthorId).ToPagedList(pageNumber, pageSize);

            ViewBag.TotalRecords = _db.Authors.Count();

            return View(authors);
        }

        // GET: Authors/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Authors.Add(author);
                    _db.SaveChanges();
                    return RedirectToAction("Create", new { message = "Author created successfully." });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the author: {ex.Message}");
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id, string message = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = _db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = message;
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(author).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Edit", new { id = author.AuthorId, message = "Author updated successfully." });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while updating the author: {ex.Message}");
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var author = _db.Authors.FirstOrDefault(a => a.AuthorId == id);
                if (author == null)
                {
                    return HttpNotFound();
                }

                _db.Authors.Remove(author);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the author: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
