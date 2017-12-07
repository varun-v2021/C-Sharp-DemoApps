using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProgrammingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            FetchFileContent();
            Console.ReadKey();
            FetchFileContent1();
            Console.ReadKey();

            //SaySomething();
            Console.Write(result);
        }

        static string ReadData() {
            FileStream fs = new FileStream(@"D:\Resource.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string data = reader.ReadToEnd();
            Thread.Sleep(5000);                         //Add System.Threading namespace to resolve the compilation error.
            return data;
        }

        static async void FetchFileContent()
        {
            //Asynchronous
            /*Console.WriteLine("Async Execution Started:");
            Task<string> t = Task.Run(() => ReadData());
            Console.WriteLine("Reading Data....");
            string result = await t;
            Console.WriteLine("\nFile Data :\n" + result); */
            /*
             Note that before completing the call to ReadData(), 
             the subsequent lines in FetchFileContent() are getting executed and
             when the await keyword is encountered, method execution is suspended and it waits for the result.
             */

            Console.WriteLine("Execution Started:");
            Task<string> t = ReadDataAsync();
            Console.WriteLine("Reading Data....");
            string result = await t;
            Console.WriteLine("\nFile Data :\n" + result);
            /*Note that before completing the call to ReadDataAsync(), 
             * the subsequent lines in FetchFileContent() are getting executed and when the await keyword is encountered,
             *  method execution is suspended and it waits for the result.
             */
        }

        //Synchronous
        static void FetchFileContent1() {            
            Console.WriteLine("Sync Execution Started:");
            string t = ReadData();
            Console.WriteLine("Reading Data....");
            string result = t;
            Console.WriteLine("\nFile Data :\n" + result);
        }

        /*
         The return type is one of the following types:
         Task if your method has a return statement in which the operand has type TResult.
         Task if your method has no return statement or has a return statement with no operand.
         Void (a Sub in Visual Basic) if you're writing an async event handler.
         */
        static async Task<string> ReadDataAsync()
        {
            FileStream fs = new FileStream(@"D:\Resource.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string data = await reader.ReadToEndAsync();
            Thread.Sleep(5000); //Performing some task
            return data;
        }


        private static string result;

        static async Task<string> SaySomething()
        {
            await Task.Delay(5);
            result = "Hello world!";
            return "something";
        }
    }
}
