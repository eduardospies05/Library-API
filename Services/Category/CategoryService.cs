using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.DTOs.Category;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext Context;

        public CategoryService(AppDbContext context)
        {
            Context = context;
        }
        public async Task<ResponseModel<IEnumerable<OutputCategoryDTO>>> GetAllAsync()
        {
            ResponseModel<IEnumerable<OutputCategoryDTO>> response = new();

            try
            {
                var list   = await Context.Categories.AsNoTracking()
                                         .OrderBy(a => a.Id)
                                         .Select(o => new OutputCategoryDTO(o.Id, o.CategoryName))
                                         .ToListAsync();

                response.Message = "All categories listed successfully";
                response.Status  =  true;
                response.Data    =  list;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputCategoryDTO>> GetByIdAsync(int id)
        {
            ResponseModel<OutputCategoryDTO> response = new();

            try
            {
                var category   = await Context.Categories
                                              .AsNoTracking()
                                              .Where(c => c.Id == id)
                                              .Select(c => new OutputCategoryDTO(
                                                c.Id,
                                                c.CategoryName
                                              )).FirstOrDefaultAsync();

                if(category == null)
                {
                    response.Message = "Category not found";
                    return response;
                }

                response.Message = $"Category '{category.CategoryName}' found";
                response.Status  =  true;
                response.Data    =  category;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputCategoryDTO>> CreateCategory(CreateCategoryDTO create)
        {
            ResponseModel<OutputCategoryDTO> response = new();

            try
            {
                CategoryModel category = new()
                {
                    CategoryName = create.CategoryName
                };

                Context.Categories.Add(category);

                await Context.SaveChangesAsync();

                response.Message = $"Category '{create.CategoryName}' created successfully";
                response.Status = true;
                response.Data = new OutputCategoryDTO(category.Id, category.CategoryName);

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputCategoryDTO>> UpdateCategory(UpdateCategoryDTO update)
        {
            ResponseModel<OutputCategoryDTO> response = new();

            try
            {
                var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id == update.Id);

                if(category == null)
                {
                    response.Message = "No category found";
                    return response;
                }

                category.CategoryName = update.CategoryName;

                await Context.SaveChangesAsync();

                response.Message = $"Category '{update.CategoryName}' updated successfully";
                response.Status = true;
                response.Data = new OutputCategoryDTO(category.Id, category.CategoryName);

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
    }
}