using AutoMapper;
using DevReviews.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            //var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //var productDetails = _mapper.Map<ProductDetailsViewModel>(product);
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody] AddProductInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateProductInputModel model)
        {
            return NoContent();
        }
    }
}