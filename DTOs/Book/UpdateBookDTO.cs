using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Book
{
    public record UpdateBookDTO (
        int Id,
        [MaxLength(100)]
        string Title,
        [Required]
        int PublisherId,
        [Required]
        int CategoryId,
        [Required]
        List<int> AuthorsId,
        [Range(0.01, double.MaxValue)]
        decimal Price
    );
}