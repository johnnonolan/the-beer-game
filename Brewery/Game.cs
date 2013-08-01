namespace Brewery
{
    public class Game
    {

        public Supplier Retailer { get; private set; }
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }

        public int Week { get; private set; }

        public Game()
        {
            Retailer = new Supplier(); 
            Wholesaler = new Supplier();
            Distributor= new Supplier();
            Factory = new Supplier();
            Week = 1;
        }

        public void EndTurn(int order)
        {
            Retailer.Inventory = Retailer.Inventory - order;
            Week++;
        }

    }
}