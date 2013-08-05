using System;
using System.Collections.Generic;

namespace Boozy
{
    public class Game
    {
        readonly IEnumerable<int> _orders;

        public Supplier Retailer { get; private set; }
        
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }
        public Guid GameId { get; private set; }
        public int Week { get; private set; }

        public Game(IEnumerable<int> orders)
        {
            _orders = orders;
            GameId = Guid.NewGuid();
            Retailer = new Supplier(null); 
            Wholesaler = new Supplier(Retailer);
            Distributor= new Supplier(Wholesaler);
            Factory = new Supplier(Distributor);
            Week = 1;
        }   

        public void EndTurn(int order, int orderForWholesaler, int orderForDistributor, int orderForFactory)
        {
            Retailer.SetOrder(order);
            Wholesaler.SetOrder(orderForWholesaler);
            Distributor.SetOrder(orderForDistributor);
            Factory.SetOrder(orderForFactory);

            Week++;
        }

    }
}