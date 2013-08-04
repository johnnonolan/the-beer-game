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

        T ModelFromActionResult<T>(ActionResult actionResult) where T : class 
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
            var actionResult = _gameController.TakeTurn(gameModel.GameId,0);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Retailer.Inventory); 
            Assert.Equal(2,model.Week); 
        }

        GameStatusViewModel CreateGame()
        {
            var createResult = _gameController.Create();
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);
            return gameModel;
        }

        [Fact]
        public void  RetailOrdersShouldBePlaced()
        {
            var gameModel = CreateGame();
            var actionResult = _gameController.TakeTurn(gameModel.GameId, 2);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.NotNull(model);
            Assert.Equal(10, model.Retailer.Inventory);
            Assert.Equal(13,model.Wholesaler.Inventory);
            Assert.Equal(2, model.Week); 
        }

        [Fact]
        public void TheNextSetOfOrdersArePending()
        {
            var gameController = new GameController(new Brewery(new[] {1, 2}));
            var createResult = gameController.Create();
            var gameModel = ModelFromActionResult<GameStatusViewModel>(createResult);
  
            var actionResult = gameController.TakeTurn(gameModel.GameId, 2);
            var model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.Equal(14, model.Retailer.Inventory);
            actionResult = gameController.TakeTurn(gameModel.GameId, 2);
            model = ModelFromActionResult<GameStatusViewModel>(actionResult);
            Assert.Equal(12, model.Retailer.Inventory);
            Assert.NotNull(model);
  
        }
    }
}