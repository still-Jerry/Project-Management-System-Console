using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management_System_Console.Views
{
    internal class ManagerView
    {
        public static string ManInfo() {
            string man = "\nman - список всех возможных команд"+"\nвведите --help для вывода справки по конкретной команде\n" + luInfo() + nuInfo() + chuInfo() + deluInfo() + ltInfo() + ltmInfo() + ntInfo() + chtInfo() + chtmInfo() + deltInfo() + clearInfo() + exitInfo();
                   
            return man;

        }
        public static string chuInfo(bool key = false)
        {

            string chu = "\nchu (change user) - изменение пользователя\n";
            string keys = "\nКлючи:" +
                            "\n\t-lo param (обязат.) - сталый логин пользователя, поскольку логин уникален, используется для идентификации изменяемого пользователя" +
                            "\n\t-ln param - новый логин пользователя" +
                            "\n\t-r param - новая роль пользователя, применяет значения а/u, где a - admin и u - user" +
                            "\n\t-p param - новая пароль пользователя"+
                            "\n\t--help - справка команды";
            if (key)
            {
                return chu + keys;
            }
            else {
                return chu;
            }
           

        }
        public static string nuInfo(bool key = false)
        {
            string nu = "\nnu (new user) - создание пользователя\n";
            string keys = "\nКлючи:" +
                            "\n\t-ln param (обязат.) - логин пользователя" +
                            "\n\t-r param (обязат.) - роль пользователя, применяет значения а/u, где a - admin и u - user" +
                            "\n\t-p param (обязат.) -  пароль пользователя"+
                              "\n\t--help - справка команды";
            if (key)
            {
                return nu + keys;
            }
            else
            {
                return nu;
            }

        }
        public static string chtInfo(bool key = false)
        {

            string cht = "\ncht (change task) - изменение задачи\n";
            string keys = "\nКлючи:" +
                            "\n\t-id param (обязат.) - номер задачи, поскольку является уникальным, используется для идентификации задачи" +
                            "\n\t-n param - новое наименование задачи" +
                            "\n\t-u param - логин нового исполнителя задачи" +
                            "\n\t-d param - новое описание задачи, применяет значения 0/1, где 1 - позволяет ввести описание задачи, 0 - оставляет описание пустым\""+
                              "\n\t--help - справка команды";
            if (key)
            {
                return cht + keys;
            }
            else
            {
                return cht;
            }


        }
        public static string ntInfo(bool key = false)
        {

            string nt = "\nnt (new task) - создание задачи, (задача создается в статусе Предложено)\n";
            string keys = "\nКлючи:" +
                            "\n\t-n param - наименование задачи" +
                            "\n\t-u param - логин исполнителя задачи" +
                            "\n\t-d param - описание задачи, применяет значения 0/1, где 1 - позволяет ввести описание задачи, 0 - оставляет описание пустым\""+
                              "\n\t--help - справка команды";
            if (key)
            {
                return nt + keys;
            }
            else
            {
                return nt;
            }


        }

        public static string ltInfo(bool key = false)
        {

            string lt = "\nlt (list tasks) - список всех задач\n";
            string keys = "\nКлючи:"+
                  "\n\t--help - справка команды";
            if (key)
            {
                return lt + keys;
            }
            else
            {
                return lt;
            }


        }
        public static string ltmInfo(bool key = false)
        {

            string lt = "\nltm (list tasks my) - список задач текущего пользователя\n";
            string keys = "\nКлючи:"+
                  "\n\t--help - справка команды";
            if (key)
            {
                return lt + keys;
            }
            else
            {
                return lt;
            }


        }
        public static string chtmInfo(bool key = false)
        {

            string lt = "\nctm (chage tasks my) - изменить статус задачи текущего пользователя\n";
            string keys = "\nКлючи:" +
                            "\n\t-id param (обязат.) - номер задачи, поскольку является уникальным, используется для идентификации задачи" +
                            "\n\t-s param (обязат.)- статус задачи 1-Предложено, 2 - В работе, 3 - Выполнено"+
                              "\n\t--help - справка команды";
            if (key)
            {
                return lt + keys;
            }
            else
            {
                return lt;
            }


        }
        public static string luInfo(bool key = false)
        {

            string lu = "\nlu (list users) - список всех пользоваетелей\n";
            string keys = "\nКлючи:"+
                  "\n\t--help - справка команды";
            if (key)
            {
                return lu + keys;
            }
            else
            {
                return lu;
            }


        }
        public static string deluInfo(bool key = false)
        {
            string delu = "\ndelu (delete user) - удаление пользователя\n";
            string keys = "\nКлючи:" +
                            "\n\t-id param - идентефикатор пользоваетеля - логин"+
                              "\n\t--help - справка команды";
            if (key)
            {
                return delu + keys;
            }
            else
            {
                return delu;
            }

        }
        public static string deltInfo(bool key = false)
        {
            string delu = "\ndelu (delete task) - удаление задачи\n";
            string keys = "\nКлючи:" +
                            "\n\t-id param - идентефикатор задачи"+
                              "\n\t--help - справка команды";
            if (key)
            {
                return delu + keys;
            }
            else
            {
                return delu;
            }

        }
        public static string clearInfo()
        {
            return "\nclear - очищение консоли\n";
        }
        public static string exitInfo()
        {
            return "\nexit - завершение работы приложения\n";
        }
    }
}
