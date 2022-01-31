using Category.Business.Abstract;
using Category.DAL.Context;
using Category.DAL.Dto.Category;
using Microsoft.EntityFrameworkCore;
using ServiceRepository.Repository;

namespace Category.Business.Concrete
{
    public class CategoryService : BaseService<DAL.Entity.Category, CategoryDto>, ICategoryService
    {
        public CategoryService(CategoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
