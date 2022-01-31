using AppCore.Entity;
using System;

namespace Product.DAL.Dto.Category
{
    public class CategoryDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
