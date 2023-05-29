using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator_Challenge.Helpers
{
    public static class ConsoleHelper
    {
        public static int ReadIntegerInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;

                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }
    }
}