using DUPL_Task_Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUPL_Task_Project.InputModels
{
    public class ReportInputModel
    {
        [Required(ErrorMessage ="From date field is required"), Display(Name ="From Date"), DataType(DataType.Date)]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "To date field is required"), Display(Name = "To Date"), DataType(DataType.Date)]
        public DateTime To { get; set; }
        public List<Book> Books { get; set; }= new List<Book>();
    }
}