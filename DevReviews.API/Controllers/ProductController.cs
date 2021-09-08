using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);
            return Ok(productsViewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetDetailsByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetails);
        }

        /// <summary>Cadastro de Produto</summary>
        /// <remarks>Requisição:
        /// {
        ///  "title": "Um chinelo top",
        ///  "description": "Um chinelo de marca",
        ///  "price": 100
        /// }
        /// </remarks>
        /// <param name="model">Objeto com dados de cadastro de Produto</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Description, model.Price);
            await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateProductInputModel model)
        {
            if (model.Description.Length > 50)
                return NotFound();

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            product.Update(model.Description, model.Price);
            await _repository.UpdateAsync(product);

            return NoContent();
        }
    }
}