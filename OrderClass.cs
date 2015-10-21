using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSOD_Assignment2
{
    class OrderClass
    {
        string senderid, receiverID;
        int amount;
        long cardNo;
        DateTime timeOfPlaced;
        public OrderClass(string senderid, long cardNo, string receiverID, int amount, DateTime timeOfPlaced)
        {
            this.senderid = senderid;
            this.cardNo = cardNo;
            this.receiverID = receiverID;
            this.amount = amount;
            this.timeOfPlaced = timeOfPlaced;
        }

        public string getOrder()
        {
            return senderid + "," + cardNo + "," + receiverID + "," + amount + "," + timeOfPlaced;
        }
    }
}
