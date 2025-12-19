using OOP_logistics_system.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Interfaces
{
    internal interface IPlanningAlgoruthm
    {
        /// <summary>
        /// Определяет стратегию планирования маршрутов курьеров.
        /// </summary>
        /// <remarks>
        /// Паттерн "Стратегия": замена аалгоритмов планирования без внесения изменений в клиентском коде.
        /// Убирает необходимость поддерживать различные алгоритмы планирования, которые могут быть легко заменены в будущем без переписывания всей системы.
        /// Реализован в классах: NaivePlanningAlgorithm, PlanValidationDecorator
        /// </remarks>
        public interface IPlanningAlgorithm
        {
            /// <summary>
            /// Генерирует маршруты для курьеров
            /// </summary>
            /// <param name="couriers">Список свобоных курьеров</param>
            /// <param name="deliveries">Список заказов на доставку</param>
            /// <param name="warehouses">Список складов</param>
            /// <param name="goods">Справочник товаров</param>
            /// <param name="orders">Словарь заказов</param>
            /// <param name="warehouseStocks">Остатки товаров на складах</param>
            /// <returns>Список маршрутов для каждого курьера</returns>
            List<WayPlan> GeneratePlans(
                List<Courier> couriers,
                List<Delivery> deliveries,
                List<Warehouse> warehouses,
                List<Good> goods,
                Dictionary<int, Order> orders,
                Dictionary<int, Dictionary<int, int>> warehouseStocks);
        }
    }
}
