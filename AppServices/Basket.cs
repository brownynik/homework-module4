using Microsoft.Extensions.DependencyInjection;
using rest_delivery.PublicContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rest_delivery.AppServices
{
    public class Basket : IBasket
    {
        private readonly List<IDishPortion> dishportions;

        public Basket()
        {
            dishportions = new List<IDishPortion>();
        }

        public void AppendDish(IDishPortion dish)
        {
            dishportions.Add(dish);
        }

        public IOrder CreateOrder(ServiceProvider provider)
        {
            // тут надо получить конкретрый экземпляр ордера
            // TODO: придумать, откуда его взять.
            IOrder o = provider.GetService<IOrder>();
            o.ConfigureOrder(this, provider);
            return o;
        }

        public bool DecrementPortion(Guid id)
        {
            IDishPortion dp = dishportions.Find(d => d.GetUID() == id);
            if (dp == null) 
            {
                return false;
            }

            // При выводе порции в 0 - удаляем товар из корзины.
            if (dp.GetPortionSize() < 2)
            {
                dishportions.Remove(dp);
            }
            else
            {
                dp.SetPortionSize(dp.GetPortionSize() - 1);
            }

            return true;
        }

        public IEnumerable<IDishPortion> Dishes()
        {
            return dishportions;
        }

        public IDishPortion GetDishByUID(Guid id)
        {
            return dishportions.Find(d => d.GetUID() == id);
        }

        public decimal GetTotalCost()
        {
            return dishportions.Sum(d => d.GetPortionSize() * d.GetCost());
        }

        public int GetTotalCount()
        {
            return dishportions.Sum(d => d.GetPortionSize());
        }

        public bool IncrementPortion(Guid id)
        {
            IDishPortion dp = dishportions.Find(d => d.GetUID() == id);
            if (dp == null)
            {
                return false;
            }

            dp.SetPortionSize(dp.GetPortionSize() + 1);
            return true;
        }

        public bool RemoveDish(Guid id)
        {
            return dishportions.Remove(dishportions.Find(d => d.GetUID() == id));
        }
    }
}
