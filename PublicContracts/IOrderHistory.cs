using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public interface IOrderHistory
    {
        IEnumerable<IOrder> Orders();
        IOrder GetOrder(Guid id);
        Guid AppendOrder(IOrder order);
        bool RemoveOrder(Guid id);
    }
}
