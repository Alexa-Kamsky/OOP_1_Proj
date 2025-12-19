using System;
using System.Collections.Generic;
using System.Net.Quic;
using System.Text;

namespace OOP_logistics_system.Domain.Entities
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string TypeTransport { get; set; }
        public double Capacity { get; set; } 
        public TimeSpan HoursStart { get; set; }
        public TimeSpan HoursEnd { get; set; }
        public double WayTimePerUnit { get; set; }
    }
}
