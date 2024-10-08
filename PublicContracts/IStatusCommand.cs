using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public interface IStatusCommand
    {
        bool ExecuteCommand(Guid orderNum, EnumOrderStatus status);
        void ConfigureOrderHistory(IOrderHistory orders);
    }
}
