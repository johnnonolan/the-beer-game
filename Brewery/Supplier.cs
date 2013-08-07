namespace Boozy
{
    public class Supplier
    {
        readonly Supplier _upstreamSupplier;

        public Supplier(Supplier upstreamSupplier)
        {
            _upstreamSupplier = upstreamSupplier;
            Inventory = 15;
            UnfulfilledOrders = 5;
            ShippingDelays = 5;
        }
        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; set; }

        public void SetOrder(int quantity)
        {
            quantity = FulfillUnfulfilledOrders(quantity);
            //send order down stream
            if (Inventory >= quantity)
                Inventory -= quantity;
            else
            {
                UnfulfilledOrders += (Inventory - quantity)*-1;
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

    }
}