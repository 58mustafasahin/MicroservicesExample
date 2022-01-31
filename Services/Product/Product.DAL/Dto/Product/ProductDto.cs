using AppCore.Entity;
using System;

namespace Product.DAL.Dto.Product
{
    public class ProductDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
