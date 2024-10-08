using Microsoft.Extensions.DependencyInjection;
using rest_delivery.PublicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rest_delivery.AppServices
{
    public class Order : IOrder
    {
        private readonly Guid _orderno;
        private EnumOrderStatus _status;
        private Guid? _recipient;
        private IBasket _basket;
        private IStatusCommand _command;

        public Order()
        {
            _orderno = Guid.NewGuid();
            _status = EnumOrderStatus.OrderCreated;
    }

        public void ConfigureOrder(IBasket basket, ServiceProvider provider)
        {
            _basket = basket;
            _command = provider.GetService<IStatusCommand>();
        }

        public bool AcceptOrder()
        {
            if (_status == EnumOrderStatus.OrderSended)
            {
                _status = EnumOrderStatus.OrderAccepted;
                Console.WriteLine($"Заказ {GetOrderNo()} получен.");
                return true;
            } else return false;
        }

        public bool CancelOrder()
        {
            _status = EnumOrderStatus.OrderCanceled;
            return true;
        }

        public bool DeleteOrder()
        {
            _status = EnumOrderStatus.OrderDeleted;
            return true;
        }

        public IBasket GetBacket()
        {
            return _basket;
        }

        public Guid GetOrderNo()
        {
            return _orderno;
        }

        public EnumOrderStatus GetStatus()
        {
            return _status;
        }

        public bool SentOrder(Guid recipient)
        {
            _recipient = recipient;
            if (_status == EnumOrderStatus.OrderPrepared)
            {
                _status = EnumOrderStatus.OrderSended;
                Console.WriteLine($"Заказ {GetOrderNo()} в процессе пересылки, ждём получения.");
                Thread.Sleep(5000);

                Console.WriteLine($"Отдаём команду на принудительное получение заказа {GetOrderNo()}.");
                return _command.ExecuteCommand(GetOrderNo(), EnumOrderStatus.OrderAccepted);
            }
            else return false;
        }

        public decimal GetTotalCost()
        {
            var basket = GetBacket();
            if (basket == null)
            {
                return 0.0m;
            }

            return basket.GetTotalCost();
        }

        public int GetTotalCount()
        {
            var basket = GetBacket();
            if (basket == null)
            {
                return 0;
            }

            return basket.GetTotalCount();
        }

        public bool PrepareOrder()
        {
            _status = EnumOrderStatus.OrderPrepared;
            Console.WriteLine($"Начинаем готовить заказ {GetOrderNo()} к отправке.");
            Thread.Sleep(5000);

            // Формируем команду на изменение статуса заказа.
            Console.WriteLine($"Отдаём команду на отправку заказа {GetOrderNo()}.");
            return _command.ExecuteCommand(GetOrderNo(), EnumOrderStatus.OrderSended);
        }

    }
}
