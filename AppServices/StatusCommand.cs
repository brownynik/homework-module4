using rest_delivery.PublicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.AppServices
{
    class StatusCommand : IStatusCommand
    {
        private IOrderHistory _orders;

        public void ConfigureOrderHistory(IOrderHistory orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            _orders = orders;
        }

        public bool ExecuteCommand(Guid orderNum, EnumOrderStatus status)
        {
            if (_orders == null)
            {
                throw new ArgumentNullException(nameof(_orders));
            }

            IOrder order = _orders.GetOrder(orderNum);
            if (order == null)
            {
                return false;
            }

            switch (status)
            {
                case EnumOrderStatus.OrderSended: return order.SentOrder(Guid.NewGuid());
                case EnumOrderStatus.OrderAccepted: return order.AcceptOrder();
                default: throw new NotImplementedException("Этот статус команда пока не обрабатывает.");
            }
        }
    }
}
