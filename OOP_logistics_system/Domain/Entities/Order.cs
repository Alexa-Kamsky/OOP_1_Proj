using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<GoodQuantity> Items { get; set; } = new List<GoodQuantity>
    }

    public class GoodQuantity
    {
        public int GoodId { get; set; }
        public int GoodQuantityId { get; set; }
    }
}