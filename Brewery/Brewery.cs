using System.Collections.Generic;
using System.Linq;

namespace Brewery
{
    public class Brewery
    {

        static List<Game> _games = new List<Game>();


        public  GameStatusViewModel CreateGame()
        {
            // set up a game
            var game = new Game();
            //save a game
            _games.Add(game);
            return  new GameStatusViewModel(game);
        }

        public GameStatusViewModel EndTurn()
        {
            // load the game
            var game=  _games.First();
            //update the game
            game.EndTurn(5);
            return new GameStatusViewModel(game);
        }
    }
}
