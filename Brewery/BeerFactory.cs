using System.Collections.Generic;

namespace Boozy
{
    public class BeerFactory : ISupplier
    { 

        /// <summary>
        /// All we need to do is 
        /// 1) process incoming deliveries to increase inventory
        /// 2) process any unfulfilled orders and send them to be delivered
        /// 3) process any fullfiled orders and send them to be delivered.
        /// 4) any orders that cannot be fullfilled are added to total
        /// 5) order from supplier (in case of this class, manufacture).
        /// </summary>
        public int Inventory { get; set; }


        public int UnfulfilledOrders { get; set; }
        public int OutwardDeliveries { get; private set; }

        public int BeingManufactured { get; private set; }

        public void SetOrder(int quantity)
        {
            FulfillUnfulfilledOrders();

            //send order down stream
            FulfillCurrentOrder(quantity);
        }



        //public void SetOrder(int quantity)
        //{
        //    Inventory = FulfillUnfulfilledOrders(Inventory);
        //    AddtoOutwardDeliveries(quantity);
        //}

        void AddtoShippingBuffers(int quantity)
        {
            OutwardDeliveries += quantity;
        }


        void FulfillUnfulfilledOrders()
        {
            if (Inventory >= UnfulfilledOrders)
            {
                AddtoShippingBuffers(UnfulfilledOrders);
                Inventory = Inventory - UnfulfilledOrders;
                UnfulfilledOrders = 0;
            }
            AddtoShippingBuffers(Inventory);
            UnfulfilledOrders -= Inventory;
            Inventory = 0;
        }

        void FulfillCurrentOrder(int quantity)
        {
            if (Inventory >= quantity)
            {
                AddtoShippingBuffers(quantity);
                Inventory -= quantity;
            }
            else
            {
                var unfulfilledOrders = (Inventory - quantity) * -1;
                AddtoShippingBuffers(Inventory);
                UnfulfilledOrders += unfulfilledOrders;
                Inventory = 0;
            }
        }


        public BeerFactory()
        {
            BeingManufactured = 0;
            Inventory = 15;
            UnfulfilledOrders = 5;
            OutwardDeliveries = 5;
        }


        public void ProcessDeliveries()
        {
            Inventory += BeingManufactured;
            BeingManufactured = 0;
        }


        public int DeliverOrders()
        {
            var sd = OutwardDeliveries;
            OutwardDeliveries = 0;
            return sd;

           // return 0;
       //     return 
        }


       //public void SetOrder(int quantity)
        //{

        //    Inventory = FulfillUnfulfilledOrders(Inventory);
        //    //send order down stream
        //    if (Inventory >= quantity)
        //    {
        //        AddtoOutwardDeliveries(quantity);
        //        Inventory -= quantity;
        //    }
        //    else
        //    {
        //        int unfulfilledOrders = (Inventory - quantity) * -1;
        //        AddtoOutwardDeliveries(Inventory);
        //        UnfulfilledOrders += unfulfilledOrders;
        //        Inventory = 0;
        //    }

        //} 

    }
}