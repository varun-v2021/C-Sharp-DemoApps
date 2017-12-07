using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandlerDemo
{
    class Program
    {
        public event EventHandler MyEvent {
            add {
                Console.WriteLine("Add operation");
            }
            remove {
                Console.WriteLine("Remove operation");
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            p.MyEvent += new EventHandler(p.DoNothing);
            p.MyEvent -= null;
        }

        void DoNothing(object Sender, EventArgs e) {
            Console.WriteLine("In doing nothing method");
        }
    }
}
