using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    /// <summary>
    /// Интерфейс еды (блюда).
    /// </summary>
    public interface IDish
    {
        Guid GetUID();
        string GetName();
        decimal GetCost();
    }
}
