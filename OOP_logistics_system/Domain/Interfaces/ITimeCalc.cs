using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Interfaces
{
    /// <summary>
    /// Калькулятор времени для логистических операций
    /// </summary>
    /// <remarks>
    /// Паттерн "Стратегия": используем алгоритмы расчета времени
    /// Поддерживает разные правила расчета времени без изменения основной логики.
    /// Реализован в классе: BasicTimeCalculator
    /// </remarks>
    public interface ITimeCalculator
    {
        /// <summary>
        /// Рассчитывает время перемещения между точками
        /// </summary>
        /// <param name="distance">Расстояние</param>
        /// <param name="travelTimePerUnit">Время на единицу расстояния для конкретного транспорта</param>
        /// <returns>Время перемещения</returns>
        TimeSpan CalculateTravelTime(int distance, double travelTimePerUnit);

        /// <summary>
        /// Считает время погрузки для такого-то количества товаров.
        /// </summary>
        /// <param name="itemCount">Количество позиций</param>
        /// <returns>Время погрузки</returns>
        TimeSpan CalculateLoadingTime(int itemCount);

        /// <summary>
        /// Считает время разгрузки для такого-то количества товаров.
        /// </summary>
        /// <param name="itemCount">Количество позиций</param>
        /// <returns>Время разгрузки</returns>
        TimeSpan CalculateUnloadingTime(int itemCount);
    }
}
