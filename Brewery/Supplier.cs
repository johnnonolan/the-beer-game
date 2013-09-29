using System;

namespace Boozy
{
    public class FurthestDownStream : Supplier
    {
        public FurthestDownStream(ISupplier upstreamSupplier)
        {
            _upstreamSupplier = upstreamSupplier;
            Inventory = 15;
            UnfulfilledOrders = 5;
           // OutwardDeliveries = 0;
        }


        public override void AddtoOutwardDeliveries(int quantity)
        {
            //void
        }
    }

    public interface ISupplier
    {
        void SetOrder(int quantity);
        void ProcessDeliveries();
        int DeliverOrders();
        int Inventory { get; set; }
        int UnfulfilledOrders { get; set; }
        int OutwardDeliveries { get; }

    }

    public class Supplier : ISupplier
    {

        static Supplier EmptySupplier()
        {
            return  new Supplier();
        }

        protected ISupplier _upstreamSupplier;


        /// <summary>
        /// All we need to do is 
        /// 1) process incoming deliveries to increase inventory
        /// 2) process any unfulfilled orders and send them to be delivered
        /// 3) process any fullfiled orders and send them to be delivered.
        /// 4) any orders that cannot be fullfilled are added to total
        /// 5) order from supplier (in case of this class, manufacture).
        /// </summary>

        public Supplier(ISupplier upstreamSupplier)
        {
            _upstreamSupplier = upstreamSupplier ?? EmptySupplier();
            Inventory = 15;
            UnfulfilledOrders = 5;
            OutwardDeliveries = 5;
        }

        protected 
            Supplier()
        {
        }

        public int Inventory { get; set; }

        public int UnfulfilledOrders { get; set; }

        public int OutwardDeliveries { get;  set ; }
  

        public virtual void AddtoOutwardDeliveries(int quantity)
        {
            OutwardDeliveries += quantity;
        }


        /// All we need to do is 
        /// 1) process incoming deliveries to increase inventory
        /// 2) process any unfulfilled orders and send them to be delivered
        /// 3) process any fullfiled orders and send them to be delivered.
        /// 4) any orders that cannot be fullfilled are added to total
        /// 5) order from supplier (in case of this class, manufacture).
        public void SetOrder(int quantity)
        {
            Inventory = FulfillUnfulfilledOrders(Inventory);
            //send order down stream
            FulfillOrders(quantity);
        }

        void FulfillOrders(int quantity)
        {
            if (Inventory >= quantity)
            {
                AddtoOutwardDeliveries(quantity);
                Inventory -= quantity;
            }
            else
            {
                int unfulfilledOrders = (Inventory - quantity)*-1;
                AddtoOutwardDeliveries(Inventory);
                UnfulfilledOrders += unfulfilledOrders;
                Inventory = 0;
            }
        }

        int FulfillUnfulfilledOrders(int inventory)
        {
            if (inventory >= UnfulfilledOrders)
            {
                AddtoOutwardDeliveries(UnfulfilledOrders);

                var remaining = inventory - UnfulfilledOrders;
                UnfulfilledOrders = 0;
                return remaining;
            }
            AddtoOutwardDeliveries(inventory);
            UnfulfilledOrders -= inventory;
            return 0;
        }


        //public void OrderFromUpStream(int qty)
        //{
        //    _upstreamSupplier.SetOrder(qty);
        //}

        public void ProcessDeliveries()
        {
            Inventory += _upstreamSupplier.DeliverOrders();
        }

        public int DeliverOrders()
        {
            var sd = OutwardDeliveries;
            OutwardDeliveries = 0;
            return sd;
        }
    }
}