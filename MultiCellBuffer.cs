using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DSOD_Assignment2
{
    class MultiCellBuffer
    {
        private int index = -1;
        private static Semaphore s = new Semaphore(2, 2);
        private string[] bufferArray = new string[2];
        public delegate void orderPlaced(int index);
        public static event orderPlaced orderPlacedEvent;
        private string cell;
        int n = 0;
        // Constructor to initialize the number of cells and the buffer array


        public void setOneCell(string cell)
        {
            // Block the cell buffer
            s.WaitOne();

            // Wait if the buffer is full
            lock (this)
            {
                while (n == 2)
                {
                    Monitor.Wait(this);
                }

                // Cell space was empty, increase the index value and fill the cell
                index++;
                bufferArray[index] = cell;
                orderPlacedEvent(index);
                // Let all other blocked threads know that this one has finished locking
            }

            s.Release();
        }
        public string getOneCell(int index)
        {

            // Block the cell buffer
            //lock (this)  lock is not required for getOneCell
            // {
            // Wait if there is nothing in the buffer
            while (n == 0)
            {
                Monitor.Wait(this);//could not find semaphore function for wait, so using monitor
            }

            // Cell was filled, fetch the cell value and reduce the index value
            cell = bufferArray[index];
            index--;

            // Let all other blocked threads know that this one has finished locking
            //Monitor.PulseAll(this);
            //}

            return cell;
        }


    }
}

