using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExpressionDemo
{
    class Product:IDisposable
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; set; }
        static int count = 1000;
        public Product()
        {
            this.ProductId = (count++);
        }

        ~Product() {
            Console.WriteLine("<<<<< Inside product class destructor >>>>>");
        }

        public void Dispose()
        {
            /*GC.SuppressFinalize() suppresses the execution of the destructor() and
             *  hence the messages from the destructor for both the objects are not displayed.*/
            GC.SuppressFinalize(this);
            Console.WriteLine("Inside Dispose()");
        }

    }
}
