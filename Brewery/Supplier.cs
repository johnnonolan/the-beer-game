namespace Brewery
{
    public class Supplier
    {

        public Supplier()
        {
            Inventory = 15;
            UnfulfilledOrders = 5;
            ShippingDelays = 5;
        }
        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; set; }
    }
}