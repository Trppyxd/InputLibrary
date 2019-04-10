using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InputLibrary;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test application started!");
            //while (true)
            //{
            //    Mouse.Move(10, 10);
            //    Thread.Sleep(10);
            //    Mouse.Move(-10, -10);
            //    Thread.Sleep(10);
            //}
            Thread asd = new Thread(() =>
            {
                Mouse.MoveSmooth(680, 634);
            });

            asd.Start();

            Console.ReadKey();
        }
    }
}
