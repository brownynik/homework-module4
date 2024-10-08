using rest_delivery.PublicContracts;
using System;
using System.Collections.Generic;

namespace rest_delivery.AppServices
{
    public class OrderHistoryInMemory : IOrderHistory
    {
        private readonly List<IOrder> orderlist;

        public OrderHistoryInMemory()
        {
            orderlist = new List<IOrder>();
        }

        public Guid AppendOrder(IOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            orderlist.Add(order);
            return order.GetOrderNo();
        }

        public IOrder GetOrder(Guid id)
        {
            return orderlist.Find(o => o.GetOrderNo() == id);
        }

        public IEnumerable<IOrder> Orders()
        {
            return orderlist;
        }

        public bool RemoveOrder(Guid id)
        {
            var order = GetOrder(id);
            if (order == null)
            {
                return false;
            }

            return orderlist.Remove(order);
        }
    }
}
