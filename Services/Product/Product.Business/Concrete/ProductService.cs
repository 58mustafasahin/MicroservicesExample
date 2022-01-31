using Product.Business.Abstract;
using Product.DAL.Context;
using Product.DAL.Dto.Product;
using ServiceRepository.Repository;

namespace Product.Business.Concrete
{
    public class ProductService :BaseService<DAL.Entity.Product, ProductDto>, IProductService
    {
        public ProductService(ProductDbContext dbContext) : base(dbContext)
        {

        }
    }
}
