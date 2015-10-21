using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace DSOD_Assignment2
{
    
    class HotelSupplier
    {
        public delegate void priceCutDelegate(Int32 p, string id);
        public event priceCutDelegate priceCutEvent;
        private Int32 oldPrice = 100;
        private static int priceCutCounter = 0;
        Random amount = new Random();
        private Int32 pricingModel()
        {
            Int32 newprice = amount.Next(50, 100);
            return newprice;
        }
        public void changePrice(Int32 newprice)
        {
            lock (this)
            {
                if (newprice < oldPrice)
                {
                    if (priceCutEvent != null)
                    {
                        priceCutCounter++;
                        priceCutEvent(newprice, Thread.CurrentThread.Name);
                        Thread.Sleep(new Random().Next(1, 10) * 100);
                    }
                }
                oldPrice = newprice;
            }

        }
        public void runHotelSupplier()
        {
            while(priceCutCounter < 10)
            {
                Int32 newprice = pricingModel();
                Console.WriteLine("New Price is ${0} for hotel {1}.", newprice, Thread.CurrentThread.Name);
                changePrice(newprice);
            }
        }

        public Int32 getPrice()
        {
            return oldPrice;
        }
        public Boolean isValid(string encyptedNo)
        {
            encryptReference.ServiceClient decryptProxy = new encryptReference.ServiceClient();
            long cardNo = Convert.ToInt64(decryptProxy.Decrypt(encyptedNo));
            return (Math.Floor(Math.Log10(cardNo) + 1) == 4);
        }
        public void newOrderPlaced(int index)
        {
            String encodedOrder = Program.mcb.getOneCell(index);
            string[] parts = EncoderDecoder.Decode(encodedOrder).Split(',');
            
            encryptReference.ServiceClient encryptProxy = new encryptReference.ServiceClient();
            string cardNo = parts[1];
            string receiverId = parts[2];
           
            
            if (isValid(encryptProxy.Encrypt(cardNo)))
            {
                DateTime completeDate = DateTime.Now;
                Console.WriteLine("Order confirmed at : {0}", completeDate);
                TimeSpan orderProcessingTime = completeDate.Subtract(Convert.ToDateTime(parts[4]));
                Console.WriteLine("Total time taken is : {0}", orderProcessingTime);
            }
            else
            {
                Console.WriteLine("Invalid Credit Card provided! Order cannot be completed!\n");
            }
        }

    }
}
