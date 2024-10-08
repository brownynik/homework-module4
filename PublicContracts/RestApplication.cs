using Microsoft.Extensions.DependencyInjection;
using rest_delivery.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public class RestApplication
    {
        /// <summary>
        /// Хранение заказов.
        /// </summary>
        private readonly IOrderHistory _orders;
        private readonly ServiceProvider _provider;

        public RestApplication(ServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _provider = provider;
            _orders = _provider.GetService<IOrderHistory>();
            IStatusCommand command = _provider.GetService<IStatusCommand>();
            command.ConfigureOrderHistory(_orders);
        }

        public IBasket CreateNewBasket()
        {
            return _provider.GetService<IBasket>();
        }

        public IOrder CreateNewOrderFromBasket(IBasket basket)
        {
            var order = basket.CreateOrder(_provider);
            Guid g = _orders.AppendOrder(order);
            IOrder o = _orders.GetOrder(g);

            return o;


            // return _orders.GetOrder();

        }


        public bool RemoveOrder(Guid id)
        {
            IOrder order = _orders.GetOrder(id);
            if (order == null)
            {
                throw new InvalidOperationException($"Ошибка удаления заказа. Заказ с Id = {id} не найден.");
            }
            
            order.DeleteOrder();
            return _orders.RemoveOrder(id);
        }

        public void publicateAllOrders()
        {
            Console.WriteLine("Перечень заказов и их статусы: ");
            foreach (IOrder order in _orders.Orders()) 
            {
                Console.WriteLine($"Заказ \"{order.GetOrderNo()}\", статус {order.GetStatus()}, полная стоимость {order.GetTotalCost()}.");
            }
        }
    }
}
