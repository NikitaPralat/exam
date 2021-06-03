using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp3
{
    class Buyer
    {
        internal bool IsAppeared { get; set; }
        internal bool IsBought { get; set; }
        internal bool IsServiced { get; set; }

        internal Buyer()
        {
            IsAppeared = false;
            IsBought = false;
            IsServiced = false;
        }

    }

    class Cash
    {
        internal Queue<Thread> BuyersQueue { get; set; }
        internal AutoResetEvent Lock { get; set; }

        internal Cash()
        {
            BuyersQueue = new Queue<Thread>();
            Lock = new AutoResetEvent(false);
        }
    }

    public class Shop
    {
        private const int count = 50;
        internal List<Thread> BuyersList { get; set; }
        internal Cash[] Cashes { get; set; }
        public Shop(int m)
        {
            BuyersList = new List<Thread>();
            Cashes = new Cash[m];
            for (int i = 0; i < Cashes.Length; i++)
                Cashes[i] = new Cash();
        }

        public void Start()
        {
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Buyer buyer = new Buyer();
                    Thread.Sleep(GaussMethod(7.0, 1.0));
                    buyer.IsAppeared = true;
                    Thread.Sleep(GaussMethod(12.0, 1.5));
                    buyer.IsBought = true;
                });
                BuyersList.Add(thread);
            }

            foreach (Thread temp in BuyersList)
                temp.Start();

            foreach (Thread temp in BuyersList)
                temp.Join();
        }

        private static int GaussMethod(double mu, double sigma)
        {
            const int n = 6;
            double dSumm = 0;
            Random ran = new Random();
            dSumm += ran.NextDouble();
            return (int)Math.Abs(Math.Round((mu + sigma * (dSumm - n / 2))));
        }
    }
}
