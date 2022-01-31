using Category.DAL.Dto.Category;
using ServiceRepository.Repository;

namespace Category.Business.Abstract
{
    public interface ICategoryService : IBaseService<CategoryDto>
    {
    }
}
