using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Project_Management_System_Console.Modules;

namespace Project_Management_System_Console.Views
{
    using BusinessModule = Modules.BusinessModule;
    internal class AuthorizationView
    {
        static string saltG = "MySalt";
        public static void LoginPwd()
        {
            try
            {
                bool check = true;
                if (SQLModule.SelectAllFrom("RoleUser") != null && SQLModule.SelectAllFrom("User") != null)
                {
                    while (check)
                    {
                        Console.Write("Логин: ");
                        string login = Console.ReadLine();
                        if (SQLModule.SelectAllFrom("user").Contains(login))
                        {
                            while (check)
                            {
                                Console.Write("Пароль: ");
                                BusinessModule.UserInfo = SQLModule.SelectAllFrom("User", " where loginUser=" + "\"" + login + "\"", join: " INNER join roleUser on roleUser.idRole = User.roleUser ");
                                string pwd = Hashing(Console.ReadLine(), BusinessModule.UserInfo[5]);
                                if (BusinessModule.UserInfo.Contains(pwd))
                                {
                                    check = false;
                                    //BusinessModule.UserInfo = SQLModule.SelectAllFrom("User");
                                    Console.Clear();
                                    Console.WriteLine("Добро пожаловать, " + login + "!\n");
                                    Console.WriteLine("введите man для получния списка всех возможных команд");
                                }
                                else
                                {
                                    Console.WriteLine("Логин и пароль не совпадают\n");
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Указанного логина не существует\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Данные о пользователях отсутсвуют");

                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }

        }

        public static string Hashing(string pwd, string saltU= "MySalt")
        {
            using (var sha256 = new SHA256Managed())
            {
                pwd = saltG + pwd + saltU;
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(pwd))).Replace("-", "");
            }

        }
    }
}
