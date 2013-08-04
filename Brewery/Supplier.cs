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
        }

        public void OrderFromUpStream(int qty)
        {
            _upstreamSupplier.SetOrder(qty);
        }

    }
}