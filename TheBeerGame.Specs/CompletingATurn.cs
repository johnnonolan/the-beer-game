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
            var actionResult = _gameController.TakeTurn(gameModel.GameId,10,0, 0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Wholesaler.Inventory); 
            Assert.Equal(2,model.Week); 
        }

        [Fact]
        public void BuffersShouldBeUpdated()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 7, 18, 6);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Retailer.UnfulfilledOrders);
            Assert.Equal(12, model.Wholesaler.UnfulfilledOrders);
            //Assert.Equal(2, model.Distributor.Inventory);
            //Assert.Equal(14, model.Factory.Inventory);
            Assert.Equal(2, model.Week);             
        }

        [Fact]
        public void  OrdersShouldBePlaced()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 7,18, 6);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(15, model.Retailer.Inventory);
            Assert.Equal(13, model.Wholesaler.Inventory);
            Assert.Equal(2, model.Distributor.Inventory);
            Assert.Equal(14,model.Factory.Inventory);
            Assert.Equal(2, model.Week); 
        }

        [Fact]
        public void TheNextSetOfOrdersArePending()
        {
            var gameController = new GameController(new Brewery(new[] {6, 2}));
            var createResult = gameController.Create();
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);
 
            //todo flesh 0's out
            var actionResult = gameController.TakeTurn(gameModel.GameId, 2,0, 0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.Equal(14, model.Retailer.Inventory);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);

            actionResult = gameController.TakeTurn(gameModel.GameId, 2,0, 0);
            model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.Equal(12, model.Retailer.Inventory);
            Assert.NotNull(model);
  
        }

        //unfullfilled orders are filled by any inventory
        //
        

        [Fact]
        public void AnyUnfullfilledOrdersThatCanBeShouldBeFulfilled()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 0, 0, 0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(15, model.Retailer.Inventory);
            Assert.Equal(0, model.Retailer.UnfulfilledOrders);
            //Assert.Equal(13, model.Wholesaler.Inventory);
            //Assert.Equal(2, model.Distributor.Inventory);
            //Assert.Equal(14, model.Factory.Inventory);
            // Assert.Equal(2, model.Week); 

        }
    }
}