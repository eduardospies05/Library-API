using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Book
{
    public record DeletedBookOutputDTO (
        int Id,
        [Required]
        [MaxLength(100)]
        string Title
    );
}