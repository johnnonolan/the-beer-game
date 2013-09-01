using System.Web.Mvc;
using Boozy;
using TheBeerGame.Controllers;
using Xunit;

namespace TheBeerGame.Specs
{
   

    //in order to update the game state and finish the game
    //


    public class StartingAGame
    {

        [Fact]
        public void SupplyChainShouldBeInitialised()
        {
            var gameController = new GameController(new Brewery());

            var actionResult=  gameController.Create();

            var vr = actionResult as ViewResult;
            Assert.NotNull(vr);

    
            Assert.IsType<GameStatusViewModel>(vr.Model);

            var model = vr.Model as GameStatusViewModel;


            Assert.NotNull(model);

            CheckSupplyChainMembersAreInitialised(model.Retailer);
            CheckSupplyChainMembersAreInitialised(model.Wholesaler);
            CheckSupplyChainMembersAreInitialised(model.Distributor);
            CheckSupplyChainMembersAreInitialised(model.Factory);
        }

// ReSharper disable UnusedParameter.Local
        static void CheckSupplyChainMembersAreInitialised(Supplier supplier)
// ReSharper restore UnusedParameter.Local
        { 
             CheckSupplyChainMembersAreInitialised(supplier, supplier is FurthestDownStream);
        }

        static void CheckSupplyChainMembersAreInitialised(Supplier supplier, bool furthestDownStream)
// ReSharper restore UnusedParameter.Local
        {
            Assert.Equal(supplier.Inventory, 15);
            Assert.Equal(supplier.UnfulfilledOrders, 5);
            var expected = 5;
            if (furthestDownStream)
            {
                expected = 0;
            }
            Assert.Equal(supplier.ShippingDelays, expected);
        }
    }

}
