using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBeerGame.Models
{
    public class GameStatusViewModel
    {
        public Supplier Retailer { get; private set; }
        public Supplier Wholesaler { get; private set; }
        public Supplier Distributor { get; private set; }
        public Supplier Factory { get; private set; }

        public GameStatusViewModel()
        {
            Retailer = new Supplier(); 
            Wholesaler = new Supplier();
            Distributor= new Supplier();
            Factory = new Supplier();
        }
    }

 

    public class Supplier
    {

        public Supplier()
        {
            Inventory = 15;
            UnfulfilledOrders = 5;
            ShippingDelays = 5;
        }
        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int ShippingDelays { get; set; }
    }
}