using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public interface IOrder
    {
        void ConfigureOrder(IBasket basket, ServiceProvider provider);

        EnumOrderStatus GetStatus();
        IBasket GetBacket();
        Guid GetOrderNo();

        bool PrepareOrder();
        /// <summary>
        /// Получатель. По хорошему, это тоже должен быть интерфейс, но для упрощения примера, берём GUID.
        /// </summary>
        /// <param name="recipient">GUID как идентификатор получателя.</param>
        bool SentOrder(Guid recipient);
        bool CancelOrder();
        bool DeleteOrder();
        bool AcceptOrder();
        
        // Полная стоимость всех товаров из корзины.
        decimal GetTotalCost();

        // Количество порций блюд.
        int GetTotalCount();
    }
}
