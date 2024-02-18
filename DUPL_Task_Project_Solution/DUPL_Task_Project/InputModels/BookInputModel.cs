using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUPL_Task_Project.InputModels
{
    public class BookInputModel
    {
        public int BookId { get; set; }
        [Required(ErrorMessage ="Book name field is required"), StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required(ErrorMessage ="Date field is required"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage ="Quantity field is required")]
        public int Quantity { get; set; }
        [Display(Name ="Author")]
        public List<int> AuthorIds { get; set; }= new List<int>();
    }
}