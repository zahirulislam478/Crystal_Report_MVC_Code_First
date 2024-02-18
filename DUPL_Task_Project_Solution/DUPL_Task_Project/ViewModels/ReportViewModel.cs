using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DUPL_Task_Project.ViewModels
{
    public class ReportViewModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Display(Name = "Author")]
        public string AuthorNames { get; set; }
    }
}