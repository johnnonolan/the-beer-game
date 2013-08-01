namespace Brewery
{
    public class GameStatusViewModel
    {
        public Supplier Retailer { get; set; }
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }

        public int Week { get; private set; }

        public GameStatusViewModel(Game game)
        {
            Retailer = game.Retailer;
            Wholesaler = game.Wholesaler;
            Distributor = game.Distributor;
            Factory = game.Factory;
            Week = game.Week;
        }


    }

 

}