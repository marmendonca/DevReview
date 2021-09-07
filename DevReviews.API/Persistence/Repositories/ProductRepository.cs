using DevReviews.API.Entities;
using DevReviews.API.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevReviews.API.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddReviewAsync(ProductReview productReview)
        {
            await _dataContext.ProductReviews.AddAsync(productReview);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dataContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetDetailsByIdAsync(int id)
        {
            return await _dataContext.Products.Include(p => p.Reviews).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductReview> GetReviewByIdAsync(int id)
        {
            return await _dataContext.ProductReviews.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _dataContext.Entry<Product>(product).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
