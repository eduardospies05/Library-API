using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Category;
using LibraryAPI.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService Service;

        public CategoryController(ICategoryService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await Service.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var response = await Service.GetByIdAsync(id);

            if(!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryDTO create)
        {
            var response = await Service.CreateCategory(create);

            if(!response.Status)
                return BadRequest(response);

            return CreatedAtAction("GetById", new { id = response.Data.Id }, response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryDTO update)
        {
            var response = await Service.UpdateCategory(update);

            if(!response.Status)
                return BadRequest();

            return Ok(response);
        }
    }
}