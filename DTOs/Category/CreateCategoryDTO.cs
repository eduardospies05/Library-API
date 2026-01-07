using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.DTOs.Category
{
    public record CreateCategoryDTO (
        [Required]
        [MaxLength(100)]
        string CategoryName
    );
}