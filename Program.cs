using Microsoft.Extensions.DependencyInjection;
using rest_delivery.AppServices;
using rest_delivery.PublicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery
{

    class Program
    {

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                       .AddSingleton<IOrderHistory, OrderHistoryInMemory>()
                       .AddSingleton<IStatusCommand, StatusCommand>()
                       .AddTransient<IBasket, Basket>()
                       .AddTransient<IOrder, Order>()
                       .BuildServiceProvider();

            RestApplication app = new RestApplication(serviceProvider);

            // Формируем новую корзину.
            IBasket basket = app.CreateNewBasket();

            // Заполняем корзину порциями блюд.
            basket.AppendDish(new DishPortion("Креветки темпура", 677.00m, 1));
            basket.AppendDish(new DishPortion("Говядина татаки", 987.00m, 1));
            basket.AppendDish(new DishPortion("Сякэ терияки", 633.00m, 2));

            // Формируем заказ из корзины.
            // Сразу добавляем его в историю заказов.
            // Теперь у нас есть заказ, содержащий зафиксированную корзину, в свою очередь, содержащую порции блюд.
            var Order = app.CreateNewOrderFromBasket(basket);

            // Console.WriteLine($"Заказ с Id = {Order.GetOrderNo()} содержит {Order.GetTotalCount()} порций блюд на общую сумму {Order.GetTotalCost()}. Статус заказа: {Order.GetStatus()}");
            app.publicateAllOrders();

            Console.WriteLine("Запускаем подготовку товара к отправке. Статусы должны пройти от обработки до получения.");
            Order.PrepareOrder();

            Console.ReadLine();
            return;
        }
    }
}
