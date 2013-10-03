using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Barbershop
{
    class Program
    {
        static Semaphore barberIsReady = new Semaphore(0, 1);
        static Semaphore accessWRSeats = new Semaphore(10, 10);
        static Semaphore custReady = new Semaphore(0, 10);
        static int numFreeSeats = 10;

        static void Main(string[] args)
        {
            Thread B = new Thread(Barber);
            Thread C1 = new Thread(Customer);
            Thread C2 = new Thread(Customer);
            Thread C3 = new Thread(Customer);
            Thread C4 = new Thread(Customer);
            Thread C5 = new Thread(Customer);
            Thread C6 = new Thread(Customer);
            Thread C7 = new Thread(Customer);
            Thread C8 = new Thread(Customer);
            Thread C9 = new Thread(Customer);
            Thread C10 = new Thread(Customer);


            B.Start();
            C1.Start();
            C2.Start();
            C3.Start();
            C4.Start();
            C5.Start();
            C6.Start();
            C7.Start();
            C8.Start();
            C9.Start();
            C10.Start();
        }

        static void Barber()
        {
            while (true)
            {
                custReady.WaitOne();
                accessWRSeats.WaitOne();
                numFreeSeats += 1;
                barberIsReady.Release();
                accessWRSeats.Release();
                Console.WriteLine("El barbero corto el pelo");
                Thread.Sleep(250);
            }
        }

        static void Customer()
        {
            while (true)
            {
                accessWRSeats.WaitOne();
                if (numFreeSeats > 0)
                {
                    numFreeSeats -= 1;
                    custReady.Release();
                    accessWRSeats.Release();
                    barberIsReady.WaitOne();
                    Console.WriteLine("El barbero corto el pelo");
                    Thread.Sleep(250);
                }

                else
                {
                    accessWRSeats.Release();
                    Console.WriteLine("Cliente se fue sin corte de pelo");
                    Console.ReadLine();
                }

            }
        }

       
    }
}
