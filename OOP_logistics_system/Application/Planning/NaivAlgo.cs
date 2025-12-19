using OOP_logistics_system.Domain.Entities;
using OOP_logistics_system.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static OOP_logistics_system.Domain.Entities.WayPlan;

namespace OOP_logistics_system.Application.Planning
{
    public class NaivePlanningAlgorithm : IPlanningAlgoruthm
    {
        private readonly IDistanceCalc _distanceCalculator;
        private const int LoadingTime = 5;
        private const int UnloadingTime = 5;

        public NaivePlanningAlgoruthm(IDistanceCalc distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public List<WayPlan> GeneratePlans(
            List<Courier> couriers,
            List<Delivery> deliveries,
            List<Warehouse> warehouses,
            List<Good> goods,
            Dictionary<int, Order> orders,
            Dictionary<int, Dictionary<int, int>> warehouseStocks)
        {
            var routePlans = new List<WayPlan>();
            var unassignedDeliveries = new List<Delivery>(deliveries);
            var goodsMap = goods.ToDictionary(g => g.Id, g => g);
            var warehouseMap = warehouses.ToDictionary(w => w.Id, w => w);

            foreach (var courier in couriers)
            {
                var currentPlan = new WayPlan { CourierId = courier.Id };
                var currentTime = courier.HoursStart;
                var currentLocation = GetNearestWarehouse(warehouses, 0, 0);
                var currentLoad = new Dictionary<int, int>();
                var remainingCapacity = courier.Capacity;

                while (unassignedDeliveries.Any() && currentTime < courier.HoursEnd)
                {
                    var nextDelivery = unassignedDeliveries
                        .OrderBy(d => _distanceCalculator.CalculateDistance(
                            currentLocation.X, currentLocation.Y, d.X, d.Y))
                        .FirstOrDefault();

                    if (nextDelivery == null)
                        break;

                    var order = orders[nextDelivery.OrderId];
                    var totalVolume = CalculateOrderVolume(order, goodsMap);

                    if (totalVolume > remainingCapacity)
                    {
                        break;
                    }

                    var fulfillmentPlan = PlanFulfillment(
                        order,
                        warehouseStocks,
                        warehouseMap,
                        goodsMap
                    );

                    if (!fulfillmentPlan.Any())
                    {
                        unassignedDeliveries.Remove(nextDelivery);
                        continue;
                    }

                    foreach (var (warehouseId, items) in fulfillmentPlan)
                    {
                        var warehouse = warehouseMap[warehouseId];

                        var distanceToWarehouse = _distanceCalculator.CalculateDistance(
                            currentLocation.X, currentLocation.Y, warehouse.X, warehouse.Y);
                        var travelTime = TimeSpan.FromMinutes(distanceToWarehouse * courier.TravelTimePerUnit);

                        currentTime += travelTime;
                        currentLocation = warehouse;

                        foreach (var (goodId, quantity) in items)
                        {
                            var good = goodsMap[goodId];
                            var itemVolume = good.Volume * quantity;

                            if (!currentLoad.ContainsKey(goodId))
                                currentLoad[goodId] = 0;

                            currentLoad[goodId] += quantity;
                            remainingCapacity -= itemVolume;

                            currentPlan.Events.Add(new WayEvent
                            {
                                Time = currentTime,
                                LocationName = warehouse.Name,
                                X = warehouse.X,
                                Y = warehouse.Y,
                                Type = EventType.PickUp,
                                Items = new List<GoodQuantity>
                                {
                                    new GoodQuantity
                                    {
                                        GoodId = goodId,
                                        Quantity = quantity,
                                        GoodName = good.Name
                                    }
                                }
                            });

                            currentTime += TimeSpan.FromMinutes(LoadingTime);
                        }
                    }

                    var distanceToClient = _distanceCalculator.CalculateDistance(
                        currentLocation.X, currentLocation.Y, nextDelivery.X, nextDelivery.Y);
                    var travelTimeToClient = TimeSpan.FromMinutes(distanceToClient * courier.TravelTimePerUnit);

                    currentTime += travelTimeToClient;
                    currentLocation = new Warehouse
                    {
                        X = nextDelivery.X,
                        Y = nextDelivery.Y,
                        Name = $"Офис '{nextDelivery.CustomerName}'"
                    };



                    var deliveryItems = new List<GoodQuantity>();
                    foreach (var item in order.Items)
                    {
                        if (currentLoad.TryGetValue(item.GoodId, out var quantity) && quantity >= item.Quantity)
                        {
                            deliveryItems.Add(new GoodQuantity
                            {
                                GoodId = item.GoodId,
                                Quantity = item.Quantity,
                                GoodName = goodsMap[item.GoodId].Name
                            });

                            currentLoad[item.GoodId] -= item.Quantity;
                            if (currentLoad[item.GoodId] == 0)
                                currentLoad.Remove(item.GoodId);

                            remainingCapacity += goodsMap[item.GoodId].Volume * item.Quantity;
                        }
                    }

                    currentPlan.Events.Add(new WayEvent
                    {
                        Time = currentTime,
                        LocationName = $"Офис '{nextDelivery.CustomerName}'",
                        X = nextDelivery.X,
                        Y = nextDelivery.Y,
                        Type = EventType.DropOff,
                        Items = deliveryItems
                    });

                    currentTime += TimeSpan.FromMinutes(UnloadingTime * deliveryItems.Count);
                }
            }
        }
    }
}