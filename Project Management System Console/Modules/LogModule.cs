
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_Management_System_Console.Modules
{
    internal class LogModule
    {
      

            public static bool WriteLog(string value, bool err=false) {
            try
            {
                string file = "";
                string user = "";
                if (err){
                    file = "//errors.log";
                }
                else {
                    file = "//steps.log";
                    user = BusinessModule.UserInfo[1];
                }
                StreamWriter writer = new StreamWriter(Environment.CurrentDirectory+ file, true);
                writer.WriteLine(DateTime.Now+" "+ user + " " + value);
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


    }
}
