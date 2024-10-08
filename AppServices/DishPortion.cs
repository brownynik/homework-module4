using rest_delivery.PublicContracts;
using System;

namespace rest_delivery.AppServices
{
    /// <summary>
    /// Порция блюда как единица в корзине.
    /// Например, 3 x "ПИЦЦА по ГАВАЙСКИ 30см".
    /// </summary>
    public class DishPortion : IDishPortion
    {
        private readonly string _shortName;
        private decimal _cost;
        private readonly Guid _id;
        private int _count;

        public DishPortion(string shortName, decimal cost, int count_in_portion)
        {
            _shortName = shortName;
            _id = Guid.NewGuid();
            _cost = cost;
            _count = count_in_portion;
        }

        public void SetNewCost(decimal newcost)
        {
            _cost = newcost;
        }

        public string GetName()
        {
            return _shortName;
        }

        public decimal GetCost()
        {
            return _cost;
        }

        public Guid GetUID()
        {
            return _id;
        }

        public int GetPortionSize()
        {
            return _count;
        }

        public void SetPortionSize(int portionSize)
        {
            if (portionSize < 0)
            {
                throw new InvalidOperationException($"Порция блюда {_shortName} не может быть отрицательной величиной.");
            }
            
            _count = portionSize;
        }

        public decimal GetPortionCost()
        {
            return GetCost() * _count;
        }
    }
}
