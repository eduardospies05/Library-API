using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Category;
using LibraryAPI.Models;

namespace LibraryAPI.Services.Category
{
    public interface ICategoryService
    {
        public Task<ResponseModel<IEnumerable<OutputCategoryDTO>>> GetAllAsync();
        public Task<ResponseModel<OutputCategoryDTO>> GetByIdAsync(int id);
        public Task<ResponseModel<OutputCategoryDTO>> CreateCategory(CreateCategoryDTO create);
        public Task<ResponseModel<OutputCategoryDTO>> UpdateCategory(UpdateCategoryDTO update);
    }
}