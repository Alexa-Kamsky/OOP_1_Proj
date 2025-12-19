using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OOP_logistics_system.Domain.Entities
{
    public class WayPlan
    {
        public TimeSpan Time { get; set; }
        public string NameLocation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public EventType Type { get; set; }

        public List<GoodQuantity> Items { get; set; } = new List<GoodQuantity>();
        public string Description => Type == EventTypeFilter.PickUp
            ? $"Забрать товар: {GetIthemsDescription()}"
            : $"Отдать товар: {GetIthemsDescription()}";

        private string GetIthemsDescription()
        {
            return string.Join(", ", GetIthemsDescription().Select(i => $"{i.Quantity} \"{i.GoodName} ({i.GoodId})\""));
        }

        public class GoodQuantity : OOP_logistics_system.Domain.Entities.GoodQuantity
        {
            public string GoodName { get; set; }
        }

        public enum EventType
        {
            PickUp,
            DropOff
        }
    }
}