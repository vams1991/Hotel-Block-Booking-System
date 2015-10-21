using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace DSOD_Assignment2
{
    class EncoderDecoder
    {
        // Encode the order
        public static string Encode(string order)
        {
            // Encrypt the order and send to the retailers
            string encryptedOrder = Encrypt(order, "ABCDEFGHIJKLMNOP");
            Console.WriteLine("Order Encrypted" , order, encryptedOrder);

            return encryptedOrder;
            
        }

        // Decode the order
        public static string Decode(string order) 
        {
            string decryptedOrder = Decrypt(order, "ABCDEFGHIJKLMNOP");
            // Debugging
            Console.WriteLine("Order Decrypted", order, decryptedOrder);
            string[] orderParts = decryptedOrder.Split(',');

            OrderClass newOrder = new OrderClass(orderParts[0], Convert.ToInt64(orderParts[1]), orderParts[2], Convert.ToInt32(orderParts[3]), Convert.ToDateTime(orderParts[4]));

            return newOrder.getOrder();
        }

        // Ecrypt the Order Object
        public static string Encrypt(String input, String key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            String result = "";
            try
            {
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        // Decrypt the Order Object
        public static String Decrypt(String input, string key)
        {
            Byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
