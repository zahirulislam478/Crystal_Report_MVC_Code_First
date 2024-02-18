using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DUPL_Task_Project.Models
{
    public class Book
    {
        public int BookId {  get; set; }
        [Required, StringLength(50), Display(Name ="Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName ="date"), DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime? Date { get; set; }
        [Required]
        public int Quantity { get; set; }   
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }= new List<BookAuthor>();
    }
    public class Author
    {
        public int AuthorId { get; set; }
        [Required, StringLength(50), Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required, StringLength(150), Display(Name = "Brief Profile")]
        public string BriefProfile { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
    public class BookAuthor
    {
        [Key, Column(Order =0), ForeignKey("Book")]
        public int BookId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }  
    }
    public class BookDbContext: DbContext
    {
        public BookDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Book> Books { get; set;}
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
    }
    public class DbInitializer: DropCreateDatabaseIfModelChanges<BookDbContext>
    {
        protected override void Seed(BookDbContext context)
        {
            if (!context.Authors.Any())
            {
                Author a1 = new Author { AuthorName = "GN Shaw", BriefProfile = "Bernard Shaw, was an Irish playwright, critic, polemicist and political activist." };
                Author a2 = new Author { AuthorName = "Franz Kalka", BriefProfile = "German-speaking Bohemian Jewish novelist and short-story writer based in Prague." };
                Author a3 = new Author { AuthorName = "Guy the Maupassaul", BriefProfile = "19th-century French author, celebrated as a master of the short story" };
                context.Authors.AddRange(new[] { a1, a2, a3 });
                Book b1 = new Book { BookName = "Man and Superman", Date = DateTime.Parse("2023-02-01"), Quantity = 1 };
                b1.BookAuthors.Add(new BookAuthor { Author = a1 });
                Book b2 = new Book { BookName = "The Castle", Date = DateTime.Parse("2022-02-01"), Quantity = 1 };
                b2.BookAuthors.Add(new BookAuthor { Author = a2 });
                Book b3 = new Book { BookName = "A Woman's Life", Date = DateTime.Parse("2024-02-01"), Quantity = 1 };
                b3.BookAuthors.Add(new BookAuthor { Author = a3 });
                context.Books.AddRange(new[] { b1, b2, b3 });
                context.SaveChanges();
            }
            
        }
    }
}