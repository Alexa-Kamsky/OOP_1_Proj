using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Entities
{
    public class Delivery
    {
        public int OrderId { get; set; }
        public string NameClient { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
