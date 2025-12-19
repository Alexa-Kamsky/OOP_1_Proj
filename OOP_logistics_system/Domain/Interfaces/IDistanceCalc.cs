using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Interfaces
{
    /// <summary>
    /// Калькулятор расстояний между местоположениями
    /// </summary>
    /// <remarks>
    /// Паттерн "Стратегия": можно использовать различные алгоритмы расчета расстояния
    /// С ним можно поддерживать разные метрики расстояния (в нашем случае манхэт.) без изменения логики планирования маршрутов.
    /// Реализован в классе: ManhattanDistanceCalculator
    /// </remarks>
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Рассчитывает расстояние между двумя местоположениями
        /// </summary>
        /// <param name="x1">Координата X точки 1</param>
        /// <param name="y1">Координата Y точки 1</param>
        /// <param name="x2">Координата X точки 2</param>
        /// <param name="y2">Координата Y точки 3</param>
        /// <returns>Расстояние между точками</returns>
        int CalculateDistance(int x1, int y1, int x2, int y2);
    }
}
