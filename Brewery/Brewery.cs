using System;
using System.Collections.Generic;

namespace Boozy
{
    public class Brewery : IBrewery
    {
        readonly Queue<int> _orders;

        static readonly Dictionary<Guid,Game> Games = new Dictionary<Guid, Game>();

        public Brewery(IEnumerable<int> orders)
        {
            _orders = new Queue<int>(orders);
        }

        public Brewery()
        {
            _orders = new Queue<int>(DefaultOrders());
        }

        private IEnumerable<int> DefaultOrders()
        {
            //fix this horridness. 
            var defaultOrders = new Queue<int>(52);

            while (defaultOrders.Count <= 52)
            {
                if (defaultOrders.Count < 6)
                {
                    defaultOrders.Enqueue(5);
                    continue;
                }
                if (defaultOrders.Count < 11)
                {
                    defaultOrders.Enqueue(7);
                    continue;
                }
                if (defaultOrders.Count < 24)
                {
                    defaultOrders.Enqueue(10);
                    continue;
                }
                if (defaultOrders.Count < 36)
                {
                    defaultOrders.Enqueue(5);
                    continue;
                }
                defaultOrders.Enqueue(defaultOrders.Count < 41 ? 1 : 8);
            }
            return defaultOrders;
        }


        public  GameStatusViewModel CreateGame()
        {
            // set up a game
            
            Game game;
            if(_orders == null)
                game = new Game(DefaultOrders());
            game = new Game(_orders);
            //save a game
            Games.Add(game.GameId,game);
            return  new GameStatusViewModel(game);
        }

        public GameStatusViewModel EndTurn(Guid gameId,  int retailOrder,
            int wholeSaleOrder, int distributorOrder)
        {
            // load the game
            var game = Games[gameId];
            //update the game
            game.EndTurn(_orders.Dequeue(),retailOrder,wholeSaleOrder, distributorOrder);
            
            return new GameStatusViewModel(game);
        }
    }
}
