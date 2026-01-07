using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public required string Title { get; set; }
        public ICollection<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
        public int CategoryId { get; set; }
        public required CategoryModel Category { get; set; }
        public int PublisherId { get; set; }
        public required PublisherModel Publisher { get; set; }
        [DataType(DataType.Currency)]
        [Precision(5,2)]
        public decimal Price { get; set; }
        public DateOnly PublishDate { get; set; }
    }
}