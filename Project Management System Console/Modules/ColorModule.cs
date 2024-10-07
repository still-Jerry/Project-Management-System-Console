using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management_System_Console.Modules
{
    internal class ColorModule
    {
        public static void ChangeColor(string text, bool err, bool writeline = true) {

            if (err)
            {
                Console.ForegroundColor = ConsoleColor.Red;

            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (writeline)
            {
                Console.WriteLine(text);
            }
            else {
                Console.Write(text);
            }
           
            Console.ResetColor();
        }
    }
}
