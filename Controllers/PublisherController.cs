using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.DTOs.Publisher;
using LibraryAPI.Services.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService Service;

        public PublisherController(IPublisherService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await Service.GetAllAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePublisher(CreatePublisherDTO create)
        {
            var response = await Service.CreatePublisher(create);

            if(!response.Status)
                return BadRequest(response);

            return CreatedAtRoute("GetPublisherById", new { id = response.Data.Id }, response);
        }

        [HttpGet("{id}", Name = "GetPublisherbyId")]
        public async Task<ActionResult> GetPublisherById(int id)
        {
            var response = await Service.GetPublisherById(id);

            if(!response.Status)
                return NotFound(response);

            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePublisher(UpdatePublisherDTO update)
        {
            var response = await Service.UpdatePublisher(update);

            if(!response.Status)
                return BadRequest(response);

            return Ok(response);
        }
    }
}