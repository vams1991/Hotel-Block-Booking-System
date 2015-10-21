using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DSOD_Assignment2
{
    class TravelAgency
    {
        static Random rng = new Random();
        Program pg = new Program();
        
        private int amount, price;
        string senderId, receiverId;
        string EncodedOrder;
        
        public void priceCut(int price, string receiverId)//eventhandler
        {
            Console.WriteLine("Price Cut event for Hotel Supplier");
            
             
                 this.price = price;
                 this.receiverId = receiverId;
                 Int64 cardNo = 1001;
                 for (int i = 0; i < 5; i++)
                 {
                     Program.agencies[i] = new Thread(() => placeorder(price, receiverId, cardNo++));
                     Program.agencies[i].Name = "TravelAgency_" + (i + 1).ToString();
                     Program.agencies[i].Start();
                 }

             
         }

        public void placeorder(int price, string receiverId, long cardNo)
        {
            this.price = price;
            amount = rng.Next(5, 10);
            senderId = Thread.CurrentThread.Name;
            DateTime currentDate = DateTime.Now;
            Console.WriteLine("Order placed by Travel Agency {0} at : {1}", Thread.CurrentThread.Name, currentDate);
            OrderClass Order = new OrderClass(senderId, cardNo, receiverId, amount, currentDate);
            EncodedOrder = EncoderDecoder.Encode(Order.getOrder());
            Program.mcb.setOneCell(EncodedOrder);
        }
    
    }
}
