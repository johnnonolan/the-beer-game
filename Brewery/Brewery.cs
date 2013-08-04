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
                    defaultOrders.Enqueue(5);
                else
                {
                    if (defaultOrders.Count < 11)
                        defaultOrders.Enqueue(7);
                    else
                    {
                        if (defaultOrders.Count < 24)
                            defaultOrders.Enqueue(10);
                        else
                        {
                            if (defaultOrders.Count < 36)
                                defaultOrders.Enqueue(5);
                            else
                            {
                                if (defaultOrders.Count < 41)
                                    defaultOrders.Enqueue(1);
                                else
                                {
                                    if (defaultOrders.Count < 53)
                                        defaultOrders.Enqueue(8);

                                }

                            }

                        }

                    }
                    
                }

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

        public GameStatusViewModel EndTurn(Guid gameId, int order, int retailOrder)
        {
            // load the game
            var game = Games[gameId];
            //update the game
            game.EndTurn(retailOrder,_orders.Dequeue());
            return new GameStatusViewModel(game);
        }
    }
}
