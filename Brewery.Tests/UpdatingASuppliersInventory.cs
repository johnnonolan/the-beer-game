using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boozy;
using Xunit;

namespace Brewery.Tests
{
    public class UpdatingASuppliersInventory
    {
        [Fact]
        public void ShouldAddOverOrdersToUnFulfilled()
        {
            var supplier = new Supplier(null);
            var inventory = supplier.Inventory;
            var unfulfilledOrders = supplier.UnfulfilledOrders;
            supplier.SetOrder(inventory+5);
            
            Assert.Equal(unfulfilledOrders+5,supplier.UnfulfilledOrders);
        }

        [Fact]
        public void ShouldReduceInventory()
        {
            var supplier = new Supplier(null);
            var inventory = supplier.Inventory;
            supplier.SetOrder(inventory);

            Assert.Equal(0,supplier.Inventory);
        }
    }
}
