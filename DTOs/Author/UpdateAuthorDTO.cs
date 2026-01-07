using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Author
{
    public record UpdateAuthorDTO (
        int Id,
        [Required]
        [MaxLength(100)]
        string FirstName,
        [Required]
        [MaxLength(255)]
        string LastName
    );
}