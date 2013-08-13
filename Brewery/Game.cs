using System;
using System.Collections.Generic;

namespace Boozy
{
    public class Game
    {
        public Supplier Retailer { get; private set; }
        
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }
        public Guid GameId { get; private set; }
        public int Week { get; private set; }

        public Game(IEnumerable<int> orders)
        {
            GameId = Guid.NewGuid();
            Factory = new Supplier(null,"Factory");
            Distributor= new Supplier(Factory,"Distributor");
            Wholesaler = new Supplier(Distributor, "Wholesaler");
            Retailer = new Supplier(Wholesaler, "Retailer");
            Week = 1;
        }   

        public void EndTurn(int order, int orderForWholesaler, int orderForDistributor, int orderForFactory)
        {
            Retailer.ProcessUpStreamOrders();
            Retailer.SetOrder(order);
            Wholesaler.ProcessUpStreamOrders();
            Wholesaler.SetOrder(orderForWholesaler);
            Distributor.ProcessUpStreamOrders();
            Distributor.SetOrder(orderForDistributor);
            Factory.ProcessUpStreamOrders();
            Factory.SetOrder(orderForFactory);

            Week++;
        }




    }
}