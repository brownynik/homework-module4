using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    /// <summary>
    ///  Корзина.
    /// </summary>
    public interface IBasket
    {
        IEnumerable<IDishPortion> Dishes();
        /// <summary>
        /// Генерация порции заказа в корзине.
        /// Корзина будет "знать" конкретный вариант блюда (реализация IDishPortion)
        /// и генерировать новую порцию.
        /// </summary>
        /// <param name="dishName">Наименование блюда.</param>
        /// <param name="dishCost">Цена блюда.</param>
        /// <param name="dishCount">Количество в порции.</param>
        /// <returns></returns>
        IDishPortion GetDishByUID(Guid id);
        void AppendDish(IDishPortion dish);
        bool RemoveDish(Guid id);
        decimal GetTotalCost();
        int GetTotalCount();
        bool IncrementPortion(Guid id);
        bool DecrementPortion(Guid id);
        /// <summary>
        /// Фиксирует корзину и генерирует заказ
        /// </summary>
        /// <returns>Отдаёт реализацию заказа.</returns>
        IOrder CreateOrder(ServiceProvider provider);
    }
}
