using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DSOD_Assignment2
{
    class Program
    {
        public static MultiCellBuffer mcb = new MultiCellBuffer();
        public static Thread hotelsupplierThread;
        public static Thread[] agencies = new Thread[5];

        static void Main(string[] args)
        {
            HotelSupplier supplier = new HotelSupplier();
            
            hotelsupplierThread = new Thread(new ThreadStart(supplier.runHotelSupplier));

            hotelsupplierThread.Name = "HotelSupplier";

            hotelsupplierThread.Start();

            TravelAgency agency = new TravelAgency();
            
            supplier.priceCutEvent += new HotelSupplier.priceCutDelegate(agency.priceCut);
            MultiCellBuffer.orderPlacedEvent += new MultiCellBuffer.orderPlaced(supplier.newOrderPlaced);
            //hotelsupplierThread.Join();
            /*for (int i = 0; i < 5; i++)
            {
                Program.agencies[i].Join();
            }*/
                Console.WriteLine("Program Completed.\nPress any key to continue!");
            Console.ReadKey();
            
        }
    }
}
