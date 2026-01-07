using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Category
{
    public record UpdateCategoryDTO (
        int Id,
        [Required]
        [MaxLength(100)]
        string CategoryName
    );
}