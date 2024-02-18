using DUPL_Task_Project.InputModels;
using DUPL_Task_Project.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace DUPL_Task_Project.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookDbContext _db = new BookDbContext();

        // GET: Books
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var books = _db.Books.OrderBy(b => b.BookId).ToPagedList(pageNumber, pageSize);

            ViewBag.TotalRecords = _db.Books.Count();

            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            ViewBag.Authors = _db.Authors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookInputModel model)
        {
            ViewBag.Authors = _db.Authors.ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    var book = new Book
                    {
                        BookName = model.BookName,
                        Date = model.Date,
                        Quantity = model.Quantity
                    };
                    foreach (var id in model.AuthorIds)
                    {
                        book.BookAuthors.Add(new BookAuthor { AuthorId = id });
                    }
                    _db.Books.Add(book);
                    _db.SaveChanges();

                    return RedirectToAction("Create", new { message = "Data insert is successful" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while saving the book: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id, string message = "")
        {
            var book = _db.Books.FirstOrDefault(x => x.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var model = new BookInputModel
            {
                BookId = book.BookId,
                BookName = book.BookName,
                Date = book.Date,
                Quantity = book.Quantity,
                AuthorIds = book.BookAuthors.Select(x => x.AuthorId).ToList()
            };
            ViewBag.Message = message;
            ViewBag.Authors = _db.Authors.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookInputModel model)
        {
            var book = _db.Books.FirstOrDefault(x => x.BookId == model.BookId);
            if (book == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    book.BookName = model.BookName;
                    book.Date = model.Date;
                    book.Quantity = model.Quantity;

                    _db.Database.ExecuteSqlCommand($"DELETE FROM BookAuthors WHERE BookId={book.BookId}");

                    foreach (var id in model.AuthorIds)
                    {
                        _db.BookAuthors.Add(new BookAuthor { BookId = book.BookId, AuthorId = id });
                    }
                    _db.SaveChanges();
                    return RedirectToAction("Edit", new { id = book.BookId, message = "Update is successful" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the book: {ex.Message}");
                }
            }
            ViewBag.Authors = _db.Authors.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(x => x.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            try
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the book: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
