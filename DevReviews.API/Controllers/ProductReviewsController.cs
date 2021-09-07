using AutoMapper;
using DevReviews.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [Route("api/products/{productId}/productsReviews")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ProductReviewsController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int productId, int id)
        {
            //var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReview);
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]AddProductReviewInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1, productId = 1 }, model); 
        }
    }
}
