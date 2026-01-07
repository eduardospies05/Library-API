using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.DTOs.Author;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext Context;

        public AuthorService(AppDbContext context)
        {
            Context = context;
        }

        public async Task<ResponseModel<IEnumerable<OutputAuthorDTO>>> GetAllAsync()
        {
            ResponseModel<IEnumerable<OutputAuthorDTO>> response = new();

            try
            {
                var authors = await Context.Authors.AsNoTracking()
                                                   .Include(a => a.Books)
                                                   .OrderBy(a => a.Id)
                                                   .Select(o => new OutputAuthorDTO(o.Id, o.FirstName, o.LastName, 
                                                   o.Books.OrderBy(b => b.Title).Select(b => b.Title).ToList()))
                                                   .ToListAsync();

                response.Message = "all Authors listed";
                response.Data    = authors;
                response.Status  = true;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputAuthorDTO>> GetByIdAsync(int id)
        {
            ResponseModel<OutputAuthorDTO> response = new();

            try
            {
                var author = await Context.Authors.AsNoTracking()
                                                  .Where(a => a.Id == id)
                                                  .Select(a => new OutputAuthorDTO(
                                                    a.Id,
                                                    a.FirstName,
                                                    a.LastName,
                                                    a.Books.OrderBy(b => b.Title).Select(b => b.Title).ToList()
                                                  )).FirstOrDefaultAsync();

                if(author == null)
                {
                    response.Message = "No author found";
                    return response;
                }

                response.Message = "all Authors listed";
                response.Data    = author;
                response.Status  = true;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputAuthorDTO>> CreateAuthor(CreateAuthorDTO create)
        {
            ResponseModel<OutputAuthorDTO> response = new();

            try
            {
                AuthorModel author = new()
                {
                    FirstName = create.FirstName,
                    LastName = create.LastName,
                };

                Context.Authors.Add(author);
                await Context.SaveChangesAsync();

                response.Message = "all Authors listed";
                response.Data    = new OutputAuthorDTO(author.Id, author.FirstName, author.LastName, new List<string>());
                response.Status  = true;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputAuthorDTO>> UpdateAuthor(UpdateAuthorDTO update)
        {
            ResponseModel<OutputAuthorDTO> response = new();

            try
            {
                AuthorModel? author = await Context.Authors.FirstOrDefaultAsync(a => a.Id == update.Id);

                if(author == null)
                {
                    response.Message = "No author found";
                    return response;
                }

                author.FirstName = update.FirstName;
                author.LastName  = update.LastName;

                await Context.SaveChangesAsync();

                response.Message = "Author updated successfully";
                response.Status = true;
                response.Data = new OutputAuthorDTO(author.Id, author.FirstName, author.LastName, author.Books.OrderBy(b => b.Title).Select(b => b.Title).ToList());

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputAuthorDTO>> DeleteByIdAsync(int id)
        {
            ResponseModel<OutputAuthorDTO> response = new();

            try
            {
                AuthorModel? author = await Context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if(author == null)
                {
                    response.Message = "Author not found";
                    return response;
                }

                Context.Authors.Remove(author);
                await Context.SaveChangesAsync();

                response.Data = new OutputAuthorDTO(author.Id, author.FirstName, author.LastName, author.Books.OrderBy(b => b.Title).Select(b => b.Title).ToList());
                response.Status = true;
                response.Message = "Author deleted successfully";

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