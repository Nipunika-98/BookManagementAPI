using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Models;


namespace BookManagementAPI.Data
{
    public class ApiContext : DbContext 
    {
        public DbSet<BookModel> Books { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
