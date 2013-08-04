using System;

namespace Boozy
{
    public class GameStatusViewModel
    {
        public Guid GameId { get; private set; }
        public Supplier Retailer { get; set; }
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }
        public int RetailOrder { get; set; }

        public int Week { get; private set; }

        public GameStatusViewModel(Game game)
        {
            GameId = game.GameId; 
            Retailer = game.Retailer;
            Wholesaler = game.Wholesaler;
            Distributor = game.Distributor;
            Factory = game.Factory;
            Week = game.Week;
        }


    }

 

}