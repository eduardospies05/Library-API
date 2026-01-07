using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.DTOs.Publisher;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services.Publisher
{
    public class PublisherService : IPublisherService
    {
        private readonly AppDbContext Context;
        public PublisherService(AppDbContext context)
        {
            Context = context;
        }

        public async Task<ResponseModel<IEnumerable<OutputPublisherDTO>>> GetAllAsync()
        {
            ResponseModel<IEnumerable<OutputPublisherDTO>> response = new();

            try
            {
                var publishers = await Context.Publishers.AsNoTracking()
                                                         .OrderBy(p => p.Id)
                                                         .Select(p => new OutputPublisherDTO(p.Id, p.PublisherName,
                                                         p.Books.OrderBy(b => b.Title).Select(b => b.Title).ToList()))
                                                         .ToListAsync();

                response.Status = true;
                response.Data   = publishers;
                response.Message = "All publishers listed";

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputPublisherDTO>> GetPublisherById(int id)
        {
            ResponseModel<OutputPublisherDTO> response = new();

            try
            {
                var publisher = await Context.Publishers.AsNoTracking()
                                                        .Where(p => p.Id == id)
                                                        .Select(p => new OutputPublisherDTO(
                                                            p.Id,
                                                            p.PublisherName,
                                                            p.Books.OrderBy(b => b.Id).Select(b => b.Title).ToList()
                                                        )).FirstOrDefaultAsync();

                if(publisher == null)
                {
                    response.Message = "No publisher found";
                    return response;
                }

                response.Data    = publisher;
                response.Status  = true;
                response.Message = "Publisher found";

                return response;                          
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OutputPublisherDTO>> CreatePublisher(CreatePublisherDTO create)
        {
            ResponseModel<OutputPublisherDTO> response = new();

            try
            {
                PublisherModel publisher = new()
                {
                    PublisherName = create.PublisherName
                };

                Context.Publishers.Add(publisher);
                await Context.SaveChangesAsync();

                response.Data = new OutputPublisherDTO(publisher.Id, publisher.PublisherName, new List<string>());
                response.Message = "Publisher created successfully";
                response.Status = true;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                return response;                
            }
        }

        public async Task<ResponseModel<OutputPublisherDTO>> UpdatePublisher(UpdatePublisherDTO update)
        {
            ResponseModel<OutputPublisherDTO> response = new();

            try
            {
                PublisherModel? publisher = await Context.Publishers.FirstOrDefaultAsync(p => p.Id == update.Id);

                if(publisher == null)
                {
                    response.Message = "No publisher found";
                    return response;
                }

                publisher.PublisherName = update.PublisherName;

                await Context.SaveChangesAsync();

                response.Data = new OutputPublisherDTO(publisher.Id, publisher.PublisherName,
                                                       publisher.Books
                                                       .OrderBy(b => b.Title)
                                                       .Select(b  => b.Title)
                                                       .ToList());
                response.Status = true;
                response.Message = "Publisher updated successfully";

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