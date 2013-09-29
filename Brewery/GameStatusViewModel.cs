using System;

namespace Boozy
{
    public class GameStatusViewModel
    {
        public Guid GameId { get; private set; }
        public ISupplier Retailer { get; set; }
        public ISupplier Wholesaler { get; private set; }
        public ISupplier Distributor { get; private set; }
        public BeerFactory Factory { get; private set; }
        public int RetailOrder { get; set; }
        public int WholesaleOrder { get; set; }
        public int DistributorOrder { get; set; }
        public int FactoryOrder { get; set; }

        public int Week { get; private set; }

        public GameStatusViewModel(Game game)
        {
            GameId = game.GameId; 
            Retailer = game.Retailer;
            Wholesaler = game.Wholesaler;
            Distributor = game.Distributor;
            Factory = (BeerFactory) game.Factory;
            Week = game.Week;
        }


    }

 

}