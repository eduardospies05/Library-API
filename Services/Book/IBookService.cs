using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Models;

namespace LibraryAPI.Services.Book
{
    public interface IBookService 
    {
        public Task<ResponseModel<IEnumerable<OutputBookDTO>>> GetAllAsync();
        public Task<ResponseModel<OutputBookDTO>> GetBookById(int id);
        public Task<ResponseModel<OutputBookDTO>> CreateBook(CreateBookDTO create);
        public Task<ResponseModel<OutputBookDTO>> UpdateBook(UpdateBookDTO update);
        public Task<ResponseModel<DeletedBookOutputDTO>> DeleteBook(int id);
    }
}