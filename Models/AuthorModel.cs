using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(255)]
        public required string LastName { get; set; }

        public ICollection<BookModel> Books { get; set; } = new List<BookModel>();
    }
}