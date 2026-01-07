using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models
{
    public class PublisherModel
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string PublisherName { get; set; }
        public ICollection<BookModel> Books { get; set; } = new List<BookModel>();
    }
}