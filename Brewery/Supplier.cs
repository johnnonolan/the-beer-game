using System;

namespace Boozy
{
    public class Supplier
    {
        readonly string _description;

        static Supplier EmptySupplier()
        {
            return  new Supplier();
        }

        readonly Supplier _upstreamSupplier;

        public Supplier(Supplier upstreamSupplier, string description)
        {
            _description = description;
            _upstreamSupplier = upstreamSupplier ?? EmptySupplier();
            Inventory = 15;
            UnfulfilledOrders = 5;
            ShippingDelays = 5;
        }

        public override string ToString()
        {
            return _description;
        }
        Supplier()
        {
        }

        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; set; }

        public void SetOrder(int quantity)
        {
            //_upstreamSupplier.ShippingDelays += quantity; 
            Inventory = FulfillUnfulfilledOrders(Inventory);
            //send order down stream
            if (Inventory >= quantity)
            {
                ShippingDelays += quantity;
                Inventory -= quantity;
            }
            else
            {
                int unfulfilledOrders = (Inventory - quantity)*-1;
                ShippingDelays += Inventory;
                UnfulfilledOrders += unfulfilledOrders;
                Inventory = 0;
            }

        }

        int FulfillUnfulfilledOrders(int inventory)
        {
            if (inventory >= UnfulfilledOrders)
            {
                AddtoShippingBuffers(UnfulfilledOrders);

                var remaining = inventory - UnfulfilledOrders;
                UnfulfilledOrders = 0;
                return remaining;
            }
            AddtoShippingBuffers(inventory);
            UnfulfilledOrders -= inventory;
            return 0;
        }

        void AddtoShippingBuffers(int unfulfilledOrders)
        {
            ShippingDelays += unfulfilledOrders;
        }

        public void OrderFromUpStream(int qty)
        {
            _upstreamSupplier.SetOrder(qty);
        }

        public void ProcessUpStreamOrders()
        {
            Inventory += _upstreamSupplier.ShippingDelays;
            _upstreamSupplier.ShippingDelays = 0;
        }
    }
}