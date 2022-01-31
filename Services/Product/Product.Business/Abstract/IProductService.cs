using Product.DAL.Dto.Product;
using ServiceRepository.Repository;

namespace Product.Business.Abstract
{
    public interface IProductService : IBaseService<ProductDto>
    {
    }
}
