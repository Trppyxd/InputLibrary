using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputLibrary;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test application started!");

            Mouse.SetCursorPos(100, 100);
            Mouse.LeftClick();

            Console.ReadKey();
        }
    }
}
