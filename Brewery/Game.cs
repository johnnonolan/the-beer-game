using System;
using System.Collections.Generic;

namespace Boozy
{
    public class Game
    {
        public ISupplier Retailer { get; private set; }
        
        public ISupplier Wholesaler { get; private set; }
        public ISupplier Distributor { get; private set; }
        public BeerFactory Factory { get; private set; }
        public Guid GameId { get; private set; }
        public int Week { get; private set; }

        public Game(IEnumerable<int> orders)
        {
            GameId = Guid.NewGuid();
            Factory = new BeerFactory();
            Distributor= new Supplier(Factory);
            Wholesaler = new Supplier(Distributor);
            Retailer = new FurthestDownStream(Wholesaler);
            Week = 1;
        }
        /// <summary>
        /// All we need to do is 
        /// 1) process incoming deliveries to increase inventory
        /// 2) process any unfulfilled orders and send them to be delivered
        /// 3) process any fullfiled orders and send them to be delivered.
        /// 4) any orders that cannot be fullfilled are added to total
        /// 5) order from supplier (in case of this class, manufacture).
        /// </summary>
        public void EndTurn(int order, int orderForWholesaler, int orderForDistributor, int orderForFactory)
        {

            Retailer.ProcessDeliveries();
            Retailer.SetOrder(order);
            Wholesaler.ProcessDeliveries();
            Wholesaler.SetOrder(orderForWholesaler);
            Distributor.ProcessDeliveries();
            Distributor.SetOrder(orderForDistributor);
            Factory.ProcessDeliveries();
            Factory.SetOrder(orderForFactory);
            Week++;
        }




    }
}