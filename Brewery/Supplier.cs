using System;

namespace Boozy
{
    public class FurthestDownStream : Supplier
    {
        public FurthestDownStream(Supplier upstreamSupplier)
        {
            _upstreamSupplier = upstreamSupplier;
            Inventory = 15;
            UnfulfilledOrders = 5;
           // ShippingDelays = 0;
        }


        public override void AddtoShippingBuffers(int quantity)
        {
            //void
        }
    }

    public class Supplier
    {

        static Supplier EmptySupplier()
        {
            return  new Supplier();
        }

        protected  Supplier _upstreamSupplier;

        public Supplier(Supplier upstreamSupplier)
        {

            _upstreamSupplier = upstreamSupplier ?? EmptySupplier();
            Inventory = 15;
            UnfulfilledOrders = 5;
            ShippingDelays = 5;
        }

        protected 
            Supplier()
        {
        }

        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; private set ; }

        public virtual void AddtoShippingBuffers(int quantity)
        {
            ShippingDelays += quantity;
        }

        public void SetOrder(int quantity)
        {
            //_upstreamSupplier.ShippingDelays += quantity; 
            Inventory = FulfillUnfulfilledOrders(Inventory);
            //send order down stream
            if (Inventory >= quantity)
            {
                AddtoShippingBuffers(quantity);
                Inventory -= quantity;
            }
            else
            {
                int unfulfilledOrders = (Inventory - quantity)*-1;
                AddtoShippingBuffers(Inventory);
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