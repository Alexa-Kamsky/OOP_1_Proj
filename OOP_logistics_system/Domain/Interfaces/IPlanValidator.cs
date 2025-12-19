using OOP_logistics_system.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_logistics_system.Domain.Interfaces
{
    /// <summary>
    /// Валидатор курьерских маршрутов
    /// </summary>
    /// <remarks>
    /// Паттерн "Декоратор": добавление функциональности валидации к базовому алгоритму
    /// Теперь можно проверять маршруты на соответствие бизнес-правилам без изменения логики
    /// Реализован в классе: PlanValidationDecorator
    /// </remarks>
    public interface IPlanValidator
    {
        /// <summary>
        /// Проверяет корректность маршрута курьера
        /// </summary>
        /// <param name="plan">Маршрут для проверки</param>
        /// <param name="courier">Информация о курьере</param>
        /// <param name="errors">Список найденных ошибок</param>
        /// <returns>True, если маршрут корректен</returns>
        bool Validate(WayPlan plan, Courier courier, out List<string> errors);
    }
}
