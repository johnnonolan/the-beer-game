using System.Web.Mvc;
using TheBeerGame.Controllers;
using TheBeerGame.Models;
using Xunit;

namespace TheBeerGame.Specs
{
   
    public class StartingAGame
    {

        [Fact]
        public void RetailerShouldBeInitialised()
        {
            var gameController = new GameController();

            var actionResult=  gameController.Create();

            var vr = actionResult as ViewResult;
            Assert.NotNull(vr);

    
            Assert.IsType<GameStatusViewModel>(vr.Model);

            var model = vr.Model as GameStatusViewModel;


            CheckSupplyChainMembersAreInitialised(model.Retailer);
            CheckSupplyChainMembersAreInitialised(model.Wholesaler);
            CheckSupplyChainMembersAreInitialised(model.Distributor);
            CheckSupplyChainMembersAreInitialised(model.Factory);
        }

        static void CheckSupplyChainMembersAreInitialised(Supplier supplier)
        {
            Assert.Equal(supplier.Inventory, 15);
            Assert.Equal(supplier.UnfulfilledOrders, 5);
            Assert.Equal(supplier.ShippingDelays, 5);
        }
    }

}
