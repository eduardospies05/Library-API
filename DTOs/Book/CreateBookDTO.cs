using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DTOs.Book
{
    public record CreateBookDTO (
        [Required]
        [MaxLength(255)]
        string Title,
        [Required]
        ICollection<int> AuthorsId,
        DateOnly PublishDate,
        [Required]
        int CategoryId,
        [Required]
        int PublisherId,
        [Range(0.01, double.MaxValue)]
        decimal Price
    );
}