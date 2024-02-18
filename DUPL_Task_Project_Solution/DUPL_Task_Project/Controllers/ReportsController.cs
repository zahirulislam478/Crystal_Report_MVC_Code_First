using DUPL_Task_Project.CrytalReportFiles;
using DUPL_Task_Project.InputModels;
using DUPL_Task_Project.Models;
using DUPL_Task_Project.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace DUPL_Task_Project.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BookDbContext _db = new BookDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReportInputModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _db.Books.AsEnumerable().Where(
                        x => x.Date.Value.Date >= model.From.Date && x.Date.Value.Date <= model.To.Date
                        ).ToList();

                    model.Books = data;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while processing the report: {ex.Message}");
            }
            return View(model);
        }

        // Action method to download the report in PDF format
        public ActionResult DownloadPdf(DateTime from, DateTime to)
        {
            try
            {
                var data = _db.Books.AsEnumerable().Where(
                        x => x.Date.Value.Date >= from.Date && x.Date.Value.Date <= to.Date
                        )
                        .Select(b => new ReportViewModel
                        {
                            BookId = b.BookId,
                            BookName = b.BookName,
                            Date = b.Date.Value,
                            AuthorNames = string.Join(", ", b.BookAuthors.Select(x => x.Author.AuthorName).ToList()),
                            Quantity = b.Quantity
                        })
                        .ToList();

                OurBookListRpt rpt = new OurBookListRpt();
                rpt.Load();
                rpt.SetDataSource(data);

                Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(s, "application/pdf");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while generating the PDF report: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // Action method to download the report in Excel format
        public ActionResult DownloadExcel(DateTime from, DateTime to)
        {
            try
            {
                var data = _db.Books.AsEnumerable().Where(
                        x => x.Date.Value.Date >= from.Date && x.Date.Value.Date <= to.Date
                        )
                        .Select(b => new ReportViewModel
                        {
                            BookId = b.BookId,
                            BookName = b.BookName,
                            Date = b.Date.Value,
                            AuthorNames = string.Join(", ", b.BookAuthors.Select(x => x.Author.AuthorName).ToList()),
                            Quantity = b.Quantity
                        })
                        .ToList();

                OurBookListRpt rpt = new OurBookListRpt();
                rpt.Load();
                rpt.SetDataSource(data);

                Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                return File(s, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while generating the Excel report: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
