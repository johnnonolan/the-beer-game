using System.Web.Mvc;
using Boozy;
using TheBeerGame.Controllers;
using Xunit;

namespace TheBeerGame.Specs
{
    public class CompletingATurn
    {
        readonly GameController _gameController;

        public CompletingATurn()
        {
            _gameController = new GameController(new Brewery());   
        }

        GameStatusViewModel CreateGame()
        {
            var createResult = _gameController.Create();
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);
            return gameModel;
        }

        static T ModelFromActionResult<T>(ActionResult actionResult) where T : class 
        {
            var vr = actionResult as ViewResult;
            Assert.NotNull(vr);
            Assert.IsType<T>(vr.Model);
            var model = vr.Model as T;
            return model;
        }

        [Fact]
        public void InventoryShouldUpdate()
        {
            var gameModel = CreateGame();
            //todo
            var actionResult = _gameController.TakeTurn(gameModel.GameId,10,0, 0,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(5, model.Wholesaler.Inventory); 
            Assert.Equal(2,model.Week); 
        }

        [Fact]
        public void BuffersShouldBeUpdated()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 7, 18, 6,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);
            Assert.Equal(0, model.Wholesaler.UnfulfilledOrders);
            Assert.Equal(0, model.Distributor.Inventory);
            Assert.Equal(4, model.Factory.Inventory);
            Assert.Equal(2, model.Week);             
        }

        [Fact]
        public void  OrdersShouldBePlaced()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 7,18, 6,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Retailer.Inventory);
            Assert.Equal(8, model.Wholesaler.Inventory);
            Assert.Equal(0, model.Distributor.Inventory);
            Assert.Equal(4,model.Factory.Inventory);
            Assert.Equal(2, model.Week); 
        }

        [Fact]
        public void TheNextSetOfOrdersArePending()
        {
            var gameController = new GameController(new Brewery(new[] {6, 2}));
            var createResult = gameController.Create();
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);
 
            //start with 15 - 6(order) = 9 + 

            //todo flesh 0's out
            var actionResult = gameController.TakeTurn(gameModel.GameId, 0,0, 0,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.Equal(9, model.Retailer.Inventory);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);

            Assert.Equal(15, model.Wholesaler.Inventory);
            Assert.Equal(0, model.Wholesaler.UnfulfilledOrders);

            


            actionResult = gameController.TakeTurn(gameModel.GameId, 0,0, 0,0);
            model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);

            Assert.Equal(12, model.Retailer.Inventory);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);

            Assert.Equal(20, model.Wholesaler.Inventory);
            Assert.Equal(0, model.Wholesaler.UnfulfilledOrders);
  
        }

        [Fact]
        public void AnyUnfullfilledOrdersThatCanBeShouldBeFulfilled()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 0, 0, 0,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Retailer.Inventory);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);
            Assert.Equal(15, model.Wholesaler.Inventory);
            Assert.Equal(15, model.Distributor.Inventory);
            Assert.Equal(10, model.Factory.Inventory);
            Assert.Equal(2, model.Week); 

        }

        [Fact]
        public void FactoryProducesOrdersWithAShippingDelayOfOne()
        {

            //Inventory = 15;
           // UnfulfilledOrders = 5;
           // OutwardDeliveries = 5;

            // I 15 - unfulfilled orders (5) + Shipping delays
            // so because factory has no upstream. inventory is failing.


           // var gameModel = CreateGame();
            var gameController = new GameController(new Brewery(new[] { 0, 0 }));
            var createResult = gameController.Create(); 
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);

           // _gameController.TakeTurn(gameModel.GameId, 0, 0, 0, 10);
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 0, 0, 0,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            //Assert.Equal(10, model.Retailer.Inventory);
            //Assert.Equal(0, model.Retailer.UnfulfilledOrders);
            //Assert.Equal(20, model.Wholesaler.Inventory);
            //Assert.Equal(20, model.Distributor.Inventory);
            Assert.Equal(10, model.Factory.Inventory);
            //Assert.Equal(3, model.Week); 

        }


    }
}