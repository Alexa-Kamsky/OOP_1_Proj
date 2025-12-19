using LogisticsPlanning.Application.Planning;
using LogisticsPlanning.Application.Services;
using LogisticsPlanning.Domain.Entities;
using LogisticsPlanning.Domain.Interfaces;
using OOP_logistics_system.Infrastructure.Adapters;
using LogisticsPlanning.Infrastructure.Factories;
using OOP_logistics_system.Application.Planning;
using OOP_logistics_system.Domain.Interfaces;
using OOP_logistics_system.Infrastruckts.Adapters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static OOP_logistics_system.Domain.Interfaces.IPlanningAlgoruthm;

namespace LogisticsPlanning
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Проверка аргументов командной строки
                if (args.Length == 0)
                {
                    Console.WriteLine("Ошибка: Не указан путь к директории с данными.");
                    Console.WriteLine("Использование: LogisticsPlanning.exe <путь_к_данным>");
                    return;
                }

                string dataDirectory = args[0];

                // Проверка существования директории
                if (!Directory.Exists(dataDirectory))
                {
                    Console.WriteLine($"Ошибка: Указанная директория не существует: {dataDirectory}");
                    return;
                }

                Console.WriteLine("Запуск системы планирования логистики...");
                Console.WriteLine($"Загрузка данных из: {dataDirectory}");

                // Настройка зависимостей (простая реализация DI)
                IFileAdapter fileAdapter = new CsvFileAdapter();
                IDistanceCalculator distanceCalculator = new ManhattanDistanceCalculator();
                ITimeCalculator timeCalculator = new BasicTimeCalculator();

                // Создание основных компонентов
                IPlanningAlgorithm planningAlgorithm = new PlanValidationDecorator(
                    new NaivePlanningAlgorithm(distanceCalculator, timeCalculator)
                );

                // Загрузка данных
                var (goods, warehouses, couriers, deliveries, orders, warehouseStocks) =
                    fileAdapter.LoadData(dataDirectory);

                Console.WriteLine($"Загружено: {couriers.Count} курьеров, {deliveries.Count} заказов");

                // Генерация маршрутов
                var routePlans = planningAlgorithm.GeneratePlans(
                    couriers,
                    deliveries,
                    warehouses,
                    goods,
                    orders,
                    warehouseStocks
                );

                // Сохранение результатов
                var deliveryAssignments = new Dictionary