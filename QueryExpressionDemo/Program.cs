using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExpressionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categoryList = new List<Category>()
    {
        new Category{CategoryName = "Sports"},
        new Category{CategoryName = "Furniture"},
        new Category{CategoryName = "Apparels"}
    };
            List<Product> productList = new List<Product>()
    {
        new Product{ProductName="Tennis Racket", CategoryId=1, Price=499.99, QuantityAvailable=50},
        new Product{ProductName="Tennis Ball", CategoryId=1, Price=100.00, QuantityAvailable=100},
        new Product{ProductName="Dining Table", CategoryId=2, Price=15000, QuantityAvailable=10},
        new Product{ProductName="Laptop Table", CategoryId=2, Price=7000.00, QuantityAvailable=15},
        new Product{ProductName="Levis Jeans", CategoryId=3, Price=2000, QuantityAvailable=100},
        new Product{ProductName="Louis Phillipe Shirt", CategoryId=3, Price=2500, QuantityAvailable=100}
    };
            //deferred execution, query variable - productListOne doesnt hold results
            var productListOne = from p in productList
                                 select p.ProductName;

            //immediate execution, query variable - productListTwo does hold results
            /*
             Here, productListOne is the query variable, which stores the query, but not the result of query. 
             This is called deferred execution. The query is executed only while iterating over the elements.
             However, a LINQ query can be forced to execute immediately and the result can be cached in query variable.
             The immediate query execution may produce the singleton values by making use of Count, Max, Average functions etc. or a sequence of values using ToList(), ToArray() etc.
             */
            var productListTwo = (from p in productList
                                 select p.ProductName).ToList();

            productList.Add(new Product { ProductName = "Monte Carlo", CategoryId = 3, Price = 2500, QuantityAvailable = 100 });
            Console.WriteLine("Deferred execution result:");
            foreach (string item in productListOne)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Immediate execution result:");
            foreach (string item in productListTwo)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Printing product names and pricing details:");
            var productListThree = from p in productList
                                   select new { p.ProductName, p.Price };

            foreach (var item in productListThree)
            {
                Console.WriteLine("Product name :" + item.ProductName + "  Price :" + item.Price);
            }

            var productListFour = from p in productList
                                  where p.Price > 1000
                                  select new { p.ProductName, p.Price };

            foreach (var item in productListFour)
            {
                Console.WriteLine("Product Name :" + item.ProductName + " \tPrice :" + item.Price);
            }

            var productListSix = from p in productList
                                 group p by p.CategoryId
                                    into g
                                 select new { CategoryId = g.Key, NumberOfProducts = g.Count() };

            var productListSeven = from p in productList
                                   join
                                   c in categoryList
                                   on p.CategoryId equals c.CategoryId
                                   select new { c.CategoryName, p.ProductName, p.Price };

            foreach (var item in productListSix)
            {
                Console.WriteLine("Category Id :" + item.CategoryId + " Number of Products" + item.NumberOfProducts);
            }

            foreach (var item in productListSeven)
            {
                Console.WriteLine("Category Id :" + item.CategoryName + " \tProduct Name :" + item.ProductName + "\tPrice : " + item.Price);
            };

            var productListEight = from p in productList
                                   group p by p.CategoryId into g
                                   join c in categoryList
                                   on g.Key equals c.CategoryId
                                   orderby g.Key ascending
                                   select new { CategoryName = c.CategoryName, Quantity = g.Sum(x => x.QuantityAvailable) };
            foreach (var item in productListEight)
            {
                Console.WriteLine("Category Id :" + item.CategoryName + " \tQuantity :" + item.Quantity);
            };

            ///////////////////////////////LAMBDA EXPRESSION AND LINQ /////////////////////////
            Console.WriteLine("////////// START LAMBDA EXPRESSION AND LINQ ////////////");
            Product productOne = new Product { ProductName = "Tennis Racket", CategoryId = 1, Price = 499.99, QuantityAvailable = 50 };
            Product productTwo = new Product { ProductName = "Tennis Racket", CategoryId = 1, Price = 499.99, QuantityAvailable = 50 };
            Product productThree = new Product { ProductName = "Tennis Ball", CategoryId = 1, Price = 100.00, QuantityAvailable = 100 };
            Product productFour = new Product { ProductName = "Dining Table", CategoryId = 2, Price = 15000, QuantityAvailable = 10 };
            Product productFive = new Product { ProductName = "Laptop Table", CategoryId = 2, Price = 7000.00, QuantityAvailable = 15 };
            Product productSix = new Product { ProductName = "Levis Jeans", CategoryId = 3, Price = 2000, QuantityAvailable = 100 };
            Product productSeven = new Product { ProductName = "Wrangler Shirt", CategoryId = 3, Price = 2500, QuantityAvailable = 100 };
            List<Product> productList1 = new List<Product>()
            {
                productOne,
                productTwo,
                productThree,
                productFour,
                productFive,
                productSix,
                productSeven
            };

            string[] productClass = { "A", "B", "C", "D", "E", "F", "G" };

            List<Product> outDatedProducts = new List<Product>()
            {
                productSix,
                productSeven
            };

            Func<Product, bool> productDel = (Product productObj) =>
            {
                if (productObj.Price > 1000)
                    return true;
                else
                    return false;
            };
            Console.WriteLine(productDel(productOne));

            /*
             Where is a LINQ Filtering operator,
             which can be called with any IEnumerable<T> sequence and accepts a Func<T, bool> predicate. 
             Hence, the productDel1 created in previous step can be passed as a parameter to the restriction operator 
             i.e. Where.
             */
            Func<Product, bool> productDel1 = productObj => productObj.Price > 1000;
            Console.WriteLine(productDel1(productOne));

            Func<Product, bool> productDel2 = productObj => productObj.Price > 1000;

            //Here, productList calls the delegate productDel2 for each of the product element in the productList
            //by using the LINQ Restriction Operator – 'Where'.
            var productListOne2 = productList.Where(productDel2);
            //OR
            var productListOne3 = productList.Where(prodObj => prodObj.Price > 1000);
            //OR
            var productListOne4 = productList.Where(x => x.Price > 1000);

            Console.WriteLine("\nProductId\tProductName\t\tPrice\tQuantityAvailable");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var item in productListOne2)
            {
                Console.WriteLine(item.ProductId + "\t\t" + item.ProductName + "\t\t" + item.Price + "\t" + item.QuantityAvailable);
            }

            //Below prints data based on productListOne3 which is an actual lambda expression
            Console.WriteLine("\nProductId\tProductName\t\tPrice\tQuantityAvailable");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var item in productListOne3)
            {
                Console.WriteLine(item.ProductId + "\t\t" + item.ProductName + "\t\t" + item.Price + "\t" + item.QuantityAvailable);
            }

            //Projection Operator (Select)
            /*
             Using LINQ with Lambda expression – 
             Where in the previous step returned a list of products whose price is greater than 1000. 
             Now using LINQ Projection Operator – Select, only price value from each product can be retrieved 
             and returned as a new list and used for further processing.
             */
            var productPriceList = productList.Where(x => x.Price > 1000).Select(y => y.Price);
            Console.WriteLine("\nPrice");
            Console.WriteLine("------");
            foreach (var item in productPriceList)
            {
                Console.WriteLine(item);
            }

            /*
             Now use the LINQ with Lambda Expression from the previous step for selecting two values productName and price. 
             The two values for each product in the list is again returned as anonymous type.
             */
            var productNameAndPriceList = productList.Where(x => x.Price > 1000).Select(x => new { x.ProductName, x.Price });
            Console.WriteLine("\nProductName\tPrice");
            Console.WriteLine("----------------------");
            foreach (var item in productNameAndPriceList)
            {
                Console.WriteLine(item.ProductName + "\t" + item.Price);
            }

            /*
             Now using LINQ with Lambda Expression, select all the productNames and prices from the list of products
             and apply LINQ Ordering Operators – OrderBy to order the products in ascending order of price.
             */
            var orderedListOne = productList.Select(x => new { x.ProductName, x.Price }).OrderBy(x => x.Price);
            Console.WriteLine("\nProductName\tPrice");
            Console.WriteLine("----------------------");
            foreach (var item in orderedListOne)
            {
                Console.WriteLine(item.ProductName + "\t" + item.Price);
            }

            var groupedList = productList.GroupBy(y => y.CategoryId).Select(g => new { g.Key, Count = g.Count() });
            Console.WriteLine("\nCategoryId\tNumberOfProducts");
            Console.WriteLine("--------------------------------");
            foreach (var item in groupedList)
            {
                Console.WriteLine(item.Key + "\t\t" + item.Count);
            }
            /*
             Now use LINQ with Lambda Expression to join two object details. 
             Use LINQ Join Operator – Join to join productList and categoryList based on the common value
             categoryId present in both the list.
             */
            var joinedList = productList.Join(categoryList,
               x => x.CategoryId,
               y => y.CategoryId,
               (x, y) => new { x, y }).
               Select(g => new { g.x.ProductName, g.x.Price, g.y.CategoryName });
            Console.WriteLine("\nProductName\t\tPrice\t\tCategoryName");
            Console.WriteLine("----------------------------------------------------");
            foreach (var item in joinedList)
            {
                Console.WriteLine(item.ProductName + "\t\t" + item.Price + "\t\t" + item.CategoryName);
            }

            /*
                partitionOne contains only first two elements,
                partitionTwo contains all the elements except first two
                partitionThree contains 3rd and 4th elements.
             */
            var partitionOne = productList.Take(2);
            var partitionTwo = productList.Skip(2);
            var partitionThree = productList.Skip(2).Take(2);

            var partitionFour = productList.TakeWhile(x => x.ProductId <= 1003);
            var partitionFive = productList.SkipWhile(x => x.ProductId <= 1003);
            var partitionSix = productList.SkipWhile(x => x.ProductId <= 1003).TakeWhile(x => x.ProductId <= 1005);

            var firstElement = productList.First();
            Console.WriteLine(firstElement.ProductName);

            var setListOne = productList.Select(g => g.ProductName).Distinct();
            var setListTwo = productList.Except(outDatedProducts);

            Console.WriteLine("#####################################################");
            /*
                A query execution is postponed until the query variable is iterated over using a looping construct like foreach statement
                They are useful while working with a database that is being frequently updated
             */
            var query = from product in productList where product.Price < 1000 select product; //Query doesn't execute here

            foreach (var prod in query) {
                Console.WriteLine(prod.ProductName);
            }

            //////////// Memory management using Dispose /////////
            /*
             It can be observed that Dispose() for both the objects productOne and productTwo are called explicitly
             thus ensuring timely release of memory. Along with Dispose(),
             the destructors() for both the objects are also called but, implicitly.
             */
            Product productOne1 = new Product();
            Product productTwo1 = new Product();
            Console.Write(">>>> Dispose call for productOne1 <<<<\n");
            productOne1.Dispose();
            Console.Write(">>>> Dispose call for productTwo1 <<<<\n");
            productTwo1.Dispose();

            Product productOne2 = new Product();
            productOne2.Dispose();
            Product productTwo2 = null;
            try
            {
                productTwo2 = new Product();
            }
            catch (Exception exObj)
            {
                Console.WriteLine(exObj.Message);
            }
            finally
            {
                productTwo2.Dispose();
            }
            Console.WriteLine("Inside Main()");

            /*
             Special C# syntax is provided to achieve the same result as explicitly calling Dispose() in a less obtrusive manner
             by the use of using. Let us see how to implement using to implicit call the Dispose().
             Replace the try..catch..finally block with 'using' as shown below:
             */
            Product productOne3 = new Product();
            productOne3.Dispose();
            Product productTwo3 = null;
            using (productTwo3 = new Product())
            {
                Console.WriteLine(productTwo3.ProductId);
            }
            Console.WriteLine("Inside Main()");

            Program pg2 = new Program();
            pg2.fileOperationsDemo();

        }

        public void fileOperationsDemo() {

            string fileName = @"D:\Program.txt";
            //FileInfo retrieves information about a given file
            FileInfo file = null;

            FileStream fileStream = null;
            StreamWriter writer = null;
            StreamReader reader = null;

            //To store the details read from a file
            string productData = null;

            if (!File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
            }
            else
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Write);
            }

            //WRITE OPERATIONS

            Product productOne = new Product { ProductName = "Tennis Racket", CategoryId = 1, Price = 499.99, QuantityAvailable = 50 };
            writer = new StreamWriter(fileStream);

            //writing Comma Seperated Values (CSV)
            writer.WriteLine("{0},{1},{2},{3},{4}", productOne.ProductId, productOne.ProductName, productOne.CategoryId, productOne.Price, productOne.QuantityAvailable);

            Console.WriteLine("Product details are saved in the file successfully!");
            //Closes the current stream writer object and also the underlying file stream object
            writer.Close();

            //READ OPERATIONS
            file = new FileInfo(fileName);

            //File can be used to check the file existence
            if (File.Exists(fileName) && file.Length > 0)
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fileStream);
                productData = reader.ReadLine();
                Console.WriteLine(productData);
            }
            else
            {
                Console.WriteLine("Sorry!! File does not exist or No contents to read");
            }
            reader.Close();
        }
    }
}

