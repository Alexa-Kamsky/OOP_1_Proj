using OOP_logistics_system.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Interfaces
{
    // <summary>
    /// Адаптер для работы с файловой системой
    /// </summary>
    /// <remarks>
    /// Паттерн "Адаптер": преобразует интерфейс работы с файлами в удобный формат.
    /// Убирает необходимость работы с различными форматами файлов (CSV, JSON, XML) без изменения
    /// логики. Замена реализации без изменения доменной модели.
    /// Реализован в классе: CsvFileAdapter
    /// </remarks>
    public interface IFileAdapter
    {
        /// <summary>
        /// Загружает данные из файлов в указанные директории
        /// </summary>
        /// <param name="directoryPath">Путь к директории с файлами данных</param>
        /// <returns>Кортеж с загруженными данными</returns>
        (List<Good> Goods,
         List<Warehouse> Warehouses,
         List<Courier> Couriers,
         List<Delivery> Deliveries,
         Dictionary<int, Order> Orders,
         Dictionary<int, Dictionary<int, int>> WarehouseStocks) LoadData(string directoryPath);

        /// <summary>
        /// Сохраняет назначения доставок в файл
        /// </summary>
        /// <param name="directoryPath">Путь к директории</param>
        /// <param name="deliveryAssignments">Словарь соответствия заказов курьерам</param>
        void SaveDeliveryAssignments(string directoryPath, Dictionary<int, int> deliveryAssignments);

        /// <summary>
        /// Сохраняет маршруты курьеров в файлы
        /// </summary>
        /// <param name="directoryPath">Путь к директории</param>
        /// <param name="routePlans">Список маршрутов курьеров</param>
        void SaveRoutePlans(string directoryPath, List<WayPlan> routePlans);
    }
}
