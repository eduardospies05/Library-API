using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LibraryAPI.Models;

namespace LibraryAPI.DTOs.Author
{
    public record OutputAuthorDTO (
        int Id,
        [Required]
        [MaxLength(100)]
        string FirstName,
        [Required]
        [MaxLength(255)]
        string LastName,
        ICollection<string> Books
    );
}