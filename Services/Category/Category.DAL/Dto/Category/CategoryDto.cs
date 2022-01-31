using AppCore.Entity;
using System;

namespace Category.DAL.Dto.Category
{
    public class CategoryDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
