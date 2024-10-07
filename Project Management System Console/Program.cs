using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management_System_Console
{
    using SQLModule = Modules.SQLModule;
    using BusinessModule = Modules.BusinessModule;
    using ColorModule = Modules.ColorModule;
    using LogModule = Modules.LogModule;
    using ManagerView = Views.ManagerView;
    using AuthorizationView = Views.AuthorizationView;
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;
            AuthorizationView.LoginPwd();
            while (isWork) {
                ColorModule.ChangeColor("\n>> ", false, false);
                string line = Console.ReadLine();
                if (!LogModule.WriteLog(line)) {
                    Console.WriteLine("Ошибка при записи в лог");
                }
                string[] command= line.Split(' ') ;
                switch (command[0]) {
                    //справочник
                    case "man":
                        
                        Console.WriteLine(ManagerView.ManInfo());
                        break;
                    //список задач
                    case "lt":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.ltInfo(true));
                        }
                        else { 
                        Console.WriteLine("\nКоличество записей: " + BusinessModule.lt("Task")+"\n");

                        }
                        break;
                    //вывести список пользователей 
                    case "lu":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.luInfo(true));
                        }
                        else
                        {
                            Console.WriteLine("\nКоличество записей: " + BusinessModule.lt("User") + "\n");
                        }
                        break;
                    //вывести список моих задач 
                    case "ltm":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.ltmInfo(true));
                        }
                        else
                        {
                            Console.WriteLine("\nКоличество записей: " + BusinessModule.lt("Task",  " where loginUser = \"" + BusinessModule.UserInfo[1] + "\"") + "\n");
                        }
                        break;
                    //изменить пользователя 
                    case "chu":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.chuInfo(true));
                        }else if (BusinessModule.UserInfo[6] == "admin")
                        {
                            if (BusinessModule.chu(line.Split('-'), false))
                            {
                                ColorModule.ChangeColor("Успешное изменение пользователя" + "\n", false);
                            }
                        }
                        else { 
                                Console.WriteLine("Данный функционал доступен лишь администратору" + "\n");
                        }
                        break;
                    //новый пользователь
                    case "nu":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.nuInfo(true));
                        }else if (BusinessModule.UserInfo[6] == "admin")
                        {
                            if (BusinessModule.chu(line.Split('-'), true))
                            {

                                ColorModule.ChangeColor("Успешное создание пользователя" + "\n", false);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Данный функционал доступен лишь администратору" + "\n");
                        };
                        break;
                    //уделить пользователя, проверить что остается 1 админ 
                    case "delu":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.deluInfo(true));
                        }else if (BusinessModule.UserInfo[6] == "admin")
                        {
                            if (BusinessModule.delu(line.Split('-'), "User"))
                            {
                                ColorModule.ChangeColor("Успешное удаление" + "\n", false);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Данный функционал доступен лишь администратору" + "\n");
                        }
                        break;
                        //изменить задачу
                    case "cht":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.chtInfo(true));
                        }else if (BusinessModule.UserInfo[6] == "admin")
                        {
                            if (BusinessModule.cht(line.Split('-'), false, false))
                            {
                                ColorModule.ChangeColor("Успешное изменение задачи" + "\n", false);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Данный функционал доступен лишь администратору" + "\n");
                        }
                        break;
                    case "chtm":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.chtmInfo(true));
                        }else if (BusinessModule.cht(line.Split('-'), false, true))
                            {

                            ColorModule.ChangeColor("Успешное изменение статуса задачи" + "\n", false);

                        }
                        break;
                    //новая задача
                    case "nt":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.ntInfo(true));
                        }else if (BusinessModule.UserInfo[6] == "admin")
                        {
                            if (BusinessModule.cht(line.Split('-'), true, false))
                            {
                                ColorModule.ChangeColor("Успешное создание задачи" + "\n", false);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Данный функционал доступен лишь администратору" + "\n");
                        };
                        break;
                        //удалить задачу
                    case "delt":
                        if (command.Contains("--help"))
                        {
                            Console.WriteLine(ManagerView.deltInfo(true));
                        }else if (BusinessModule.delu(line.Split('-'), "Task"))
                        {
                            ColorModule.ChangeColor("Успешное удаление" + "\n", false);

                        }
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    //выйти из проги 
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        if (command[0].Replace(" ", "").Replace("    ", "").Replace("\n", "").Replace("\t","") != string.Empty)
                        {
                            Console.WriteLine("Неизвестная команда");
                        }
                        break;             
                }

            }
           
            Console.ReadKey();
        }
    }
}
