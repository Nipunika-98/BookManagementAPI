using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$", ErrorMessage = "Invalid ISBN format.")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Publication Date is required.")]
        public DateTime PublicationDate { get; set; }
    }
}
