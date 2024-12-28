namespace BookManagementAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public required string Isbn { get; set; }
        public DateOnly PublicationDate { get; set; }



    }
}
