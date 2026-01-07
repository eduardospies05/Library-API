using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Book
{
    public record AuthorShortDTO(int Id, string Fullname);
    public record CategoryShortDTO(int Id, string CategoryName);
    public record PublisherShortDTO(int Id, string publisherName);
    public record OutputBookDTO (
        int Id,
        [Required]
        [MaxLength(255)]
        string Title,
        [Required]
        List<AuthorShortDTO> Authors,
        [Required]
        CategoryShortDTO Category,
        [Required]
        PublisherShortDTO Publisher,
        decimal Price
    );
}