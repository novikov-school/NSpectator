using System.Collections.Generic;

namespace SampleSpecs.Model
{
    public class VendingMachine
    {
        private double _cash;
        private Dictionary<string, int> _inventory;
        private Dictionary<string, double> _pricePoint;

        public VendingMachine()
        {
            _inventory = new Dictionary<string, int>();
            _pricePoint = new Dictionary<string, double>();
        }

        public double Cash
        {
            get { return _cash; }
        }

        public void AddInventory(string item)
        {
            if (_inventory.ContainsKey(item) == false) _inventory.Add(item, 0);

            _inventory[item] += 1;
        }

        public void Buy(string item)
        {
            _inventory[item] -= 1;
            _cash += _pricePoint[item];
        }

        public int Inventory(string item)
        {
            return _inventory[item];
        }

        public void PricePoint(string item, double amount)
        {
            _pricePoint.Add(item, amount);
        }
    }
}