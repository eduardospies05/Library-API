using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Author;
using LibraryAPI.Models;

namespace LibraryAPI.Services.Author
{
    public interface IAuthorService
    {
        public Task<ResponseModel<IEnumerable<OutputAuthorDTO>>> GetAllAsync();
        public Task<ResponseModel<OutputAuthorDTO>> GetByIdAsync(int id);
        public Task<ResponseModel<OutputAuthorDTO>> CreateAuthor(CreateAuthorDTO create);
        public Task<ResponseModel<OutputAuthorDTO>> UpdateAuthor(UpdateAuthorDTO update);
        public Task<ResponseModel<OutputAuthorDTO>> DeleteByIdAsync(int id);
    }
}