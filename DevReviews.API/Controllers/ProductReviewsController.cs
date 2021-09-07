using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevReviews.API.Controllers
{
    [Route("api/products/{productId}/productsReviews")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductReviewsController(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int productId, int id)
        {
            var productReview = await _repository.GetReviewByIdAsync(id);

            if (productReview == null)
                return NotFound();

            var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReview);
            return Ok(productDetails);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(int productId, [FromBody]AddProductReviewInputModel model)
        {
            var productReview = new ProductReview(model.Author, model.Rating, model.Comments, productId);
            await _repository.AddReviewAsync(productReview);
            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId }, model);
        }
    }
}
