using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService Service;
        public BookController(IBookService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await Service.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await Service.GetBookById(id);

            if(!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(CreateBookDTO create)
        {
            var response = await Service.CreateBook(create);

            if(!response.Status)
                return BadRequest(response);

            return CreatedAtRoute("GetBookById", new { id = response.Data.Id }, response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook(UpdateBookDTO update)
        {
            var response = await Service.UpdateBook(update);
            
            if(!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookById(int id)
        {
            var response = await Service.DeleteBook(id);

            if(!response.Status)
                return BadRequest(response);

            return Ok(response);
        }
    }
}