using Boozy;
using Xunit;

namespace Brewery.Tests
{
    public class SettingAnOrder
    {

        [Fact]
        public void OrdersShouldAddToBufferToGoDownStream()
        {
            var upstream = new Supplier(null);
            var shippingDelays = upstream.ShippingDelays;
            var downstream = new Supplier(upstream);
            var upStreamUnfullfilled = upstream.UnfulfilledOrders;
            downstream.OrderFromUpStream(1);
            
            Assert.Equal(shippingDelays+upStreamUnfullfilled+1, upstream.ShippingDelays);
            
        }
        [Fact]
        public void ShouldAddOverOrdersToUnFulfilled()
        {
            var supplier = new Supplier(null);
            var inventory = supplier.Inventory;
            var unfulfilledOrders = supplier.UnfulfilledOrders;
            supplier.SetOrder(inventory-unfulfilledOrders+5);
            
            Assert.Equal(5,supplier.UnfulfilledOrders);
        }

        [Fact]
        public void ShouldReduceInventory()
        {
            var supplier = new Supplier(null);
            var inventory = supplier.Inventory+supplier.UnfulfilledOrders;
            supplier.SetOrder(inventory);

            Assert.Equal(0,supplier.Inventory);
        }
    }
}
