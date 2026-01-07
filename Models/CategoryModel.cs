using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class CategoryModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string CategoryName { get; set; }
    }
}