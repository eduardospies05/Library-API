using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Author;
using LibraryAPI.Services.Author;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService Service;

        public AuthorController(IAuthorService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await Service.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var response = await Service.GetByIdAsync(id);

            if(!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(CreateAuthorDTO create)
        {
            var response = await Service.CreateAuthor(create);

            if(!response.Status)
                return BadRequest(response);

            return CreatedAtRoute("GetAuthorById", new { id = response.Data.Id }, response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAuthor(UpdateAuthorDTO update)
        {
            var response = await Service.UpdateAuthor(update);

            if(!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorById(int id)
        {
            var response = await Service.DeleteByIdAsync(id);

            if(!response.Status)
                return BadRequest(response);

            return Ok(response);
        }
    }
}