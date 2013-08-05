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
        public int Inventory { get; private set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; set; }

        public void SetOrder(int quantity)
        {
            //send order down stream
            if (Inventory >= quantity)
                Inventory -= quantity;
            else
            {
                UnfulfilledOrders += (Inventory - quantity)*-1;
                Inventory = 0;
                
            }

        }

        public void OrderFromUpStream(int qty)
        {
            _upstreamSupplier.SetOrder(qty);
        }

    }
}