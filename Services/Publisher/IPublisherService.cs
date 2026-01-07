using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Publisher;
using LibraryAPI.Models;

namespace LibraryAPI.Services.Publisher
{
    public interface IPublisherService
    {
        public Task<ResponseModel<IEnumerable<OutputPublisherDTO>>> GetAllAsync();
        public Task<ResponseModel<OutputPublisherDTO>> GetPublisherById(int id);
        public Task<ResponseModel<OutputPublisherDTO>> CreatePublisher(CreatePublisherDTO create);
        public Task<ResponseModel<OutputPublisherDTO>> UpdatePublisher(UpdatePublisherDTO update);
    }
}