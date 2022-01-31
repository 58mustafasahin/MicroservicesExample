using AppCore.Entity;
using System;

namespace Product.DAL.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
