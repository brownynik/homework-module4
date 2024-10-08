using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_delivery.PublicContracts
{
    public interface IDishPortion : IDish
    {
        int GetPortionSize();
        void SetPortionSize(int portionSize);
        decimal GetPortionCost();
    }
}
