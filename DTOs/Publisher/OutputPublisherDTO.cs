using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Publisher
{
    public record OutputPublisherDTO (
        int Id,
        [Required]
        [MaxLength(255)]
        string PublisherName,
        ICollection<string> Books
    );
}