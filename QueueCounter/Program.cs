using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QueueCounter
{
    class Program
    {
        int queue = 5;
        int beingHandled = 0;
        int msBeforeNewCustomer = 10000;

        Random rand = new Random();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            Console.WriteLine("Start queue: {0}", queue);

            Thread clerk = new Thread(BootClerk);
            //Thread clerk2 = new Thread(BootClerk);
            Thread customers = new Thread(NewCustomer);

            clerk.Start();
            //clerk2.Start();
            customers.Start();
        }

        private void BootClerk()
        {
            while (true)
            {
                ServeQueue();
            }
        }

        private void ServeQueue()
        {
            if (beingHandled == queue)
                TakeBreak();
            else
            {
                beingHandled++;
                Console.WriteLine("Now handling: {0}", beingHandled);
                Thread.Sleep(5000);
            }
        }

        private void TakeBreak()
        {
            int breakTime = rand.Next(5000, 20000);
            Console.WriteLine("No customers to serve, taking a break for {0}ms", breakTime);
            Thread.Sleep(breakTime);
        }

        private void NewCustomer()
        {
            while (true)
            {
                Thread.Sleep(msBeforeNewCustomer);
                queue++;
                Console.WriteLine("Customer picked #{0}", queue);
            }
        }
    }
}
