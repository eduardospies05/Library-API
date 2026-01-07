using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.DTOs.Author;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Book
{
    public class BookService : IBookService
    {
        private readonly AppDbContext Context;

        public BookService(AppDbContext context)
        {
            Context = context;
        }

        public async Task<ResponseModel<IEnumerable<OutputBookDTO>>> GetAllAsync()
        {
            ResponseModel<IEnumerable<OutputBookDTO>> response = new();

            try
            {
                var books = await Context.Books.AsNoTracking()
                                                .OrderBy(b => b.Title)
                                                .Select(b => new OutputBookDTO(b.Id, b.Title,
                                                        b.Authors.OrderBy(a => a.Id)
                                                .Select(b => new AuthorShortDTO(b.Id, b.FirstName + " " + b.LastName)).ToList(),
                                                    new CategoryShortDTO(b.Category.Id, b.Category.CategoryName), 
                                                    new PublisherShortDTO(b.Publisher.Id, b.Publisher.PublisherName), 
                                                    b.Price)).ToListAsync();
                response.Status = true;
                response.Message = "Books found";
                response.Data = books;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputBookDTO>> GetBookById(int id)
        {
            ResponseModel<OutputBookDTO> response = new();

            try
            {
                OutputBookDTO? book = await Context.Books.AsNoTracking()
                                                         .Where(b => b.Id == id)
                                                         .Select(b => new OutputBookDTO(
                                                            b.Id,
                                                            b.Title,
                                                            b.Authors.OrderBy(a => a.Id)
                                                                        .Select(a => new AuthorShortDTO(
                                                                            a.Id,
                                                                            a.FirstName + " " + a.LastName
                                                                        )).ToList(),
                                                            new CategoryShortDTO(
                                                                b.CategoryId, b.Category.CategoryName
                                                            ),
                                                            new PublisherShortDTO(
                                                                b.PublisherId, b.Publisher.PublisherName
                                                            ),
                                                            b.Price
                                                         )).FirstOrDefaultAsync();

                if(book == null)
                {
                    response.Message = "No book found";
                    return response;
                }

                response.Status = true;
                response.Data = book;
                response.Message = "Book found";
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputBookDTO>> CreateBook(CreateBookDTO create)
        {
            ResponseModel<OutputBookDTO> response = new();
            try
            {
                List<AuthorModel>? authors = await Context.Authors.OrderBy(a => a.Id)
                                                                  .Where(a => create.AuthorsId.Contains(a.Id))
                                                                  .ToListAsync();

                PublisherModel? publisher  = await Context.Publishers
                                                          .FirstOrDefaultAsync(p => p.Id == create.PublisherId);
                CategoryModel? category    = await Context.Categories
                                                          .FirstOrDefaultAsync(c => c.Id == create.CategoryId);

                if(authors == null || publisher == null || category == null)
                {
                    response.Message = "Error creating book";
                    return response;
                }

                BookModel book = new()
                {
                      Title = create.Title,
                      Authors = authors,
                      Category = category,
                      Price = create.Price,
                      PublishDate = create.PublishDate,
                      Publisher = publisher
                };

                Context.Books.Add(book);

                await Context.SaveChangesAsync();

                response.Message = "Book created successfully";
                response.Status = true;
                response.Data = new OutputBookDTO(book.Id, 
                                                  book.Title,
                                                  book.Authors.OrderBy(a => a.Id)
                                                              .Select(a => new 
                                                              AuthorShortDTO(a.Id, a.FirstName + " " + a.LastName))
                                                              .ToList(),
                                                  new CategoryShortDTO(book.CategoryId, book.Category.CategoryName),
                                                  new PublisherShortDTO(book.PublisherId, book.Publisher.PublisherName),
                                                  book.Price 
                                                );
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;                
            }
        }

        public async Task<ResponseModel<OutputBookDTO>> UpdateBook(UpdateBookDTO update)
        {
            ResponseModel<OutputBookDTO> response = new();
            try
            {
                List<AuthorModel>? authors = await Context.Authors.Where(a => update.AuthorsId.Contains(a.Id)).ToListAsync();
                PublisherModel? publisher  = await Context.Publishers.FirstOrDefaultAsync(p => p.Id == update.PublisherId);                                        
                CategoryModel? category    = await Context.Categories.FirstOrDefaultAsync(c => c.Id == update.CategoryId);
                BookModel? book            = await Context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == update.Id);

                if(book == null)
                {
                    response.Message = "No book found";
                    return response;
                }

                if(authors == null || authors.Count != update.AuthorsId.Count || publisher == null || category == null)
                {
                    response.Message = "Error updating book";
                    return response;
                }

                book.Publisher = publisher;
                book.Category = category;
                book.Title = update.Title;
                book.Price = update.Price;

                book.Authors.Clear();

                foreach(AuthorModel a in authors)
                    book.Authors.Add(a);

                await Context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Book updated successfully";
                response.Data = new OutputBookDTO(book.Id, 
                                                  book.Title,
                                                  book.Authors.OrderBy(a => a.Id)
                                                              .Select(a => new AuthorShortDTO(a.Id, 
                                                                                               a.FirstName + " " + a.LastName)).ToList(),
                                                  new CategoryShortDTO(category.Id, category.CategoryName),
                                                  new PublisherShortDTO(publisher.Id, publisher.PublisherName),
                                                  book.Price);
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<DeletedBookOutputDTO>> DeleteBook(int id)
        {
            ResponseModel<DeletedBookOutputDTO> response = new();

            try
            {
                BookModel? book = await Context.Books.FirstOrDefaultAsync(b => b.Id == id);

                if(book == null)
                {
                    response.Message = "Book not found";
                    return response;
                }

                Context.Books.Remove(book);

                await Context.SaveChangesAsync();

                response.Data = new DeletedBookOutputDTO(book.Id, book.Title);
                response.Status = true;
                response.Message = "Book deleted successfully";

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