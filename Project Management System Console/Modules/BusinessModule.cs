using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Project_Management_System_Console.Views;

namespace Project_Management_System_Console.Modules
{
    internal class BusinessModule
    {
        public static List<String> UserInfo = new List<String>();
        public static int lt(string table, string where="")
        {
            try
            {
                List<String> list = new List<String>();
                int col = 0;
                if (table == "User")
                {
                    list = SQLModule.SelectAllFrom("User", attributes: " loginUser, nameRole ", join: " INNER join roleUser on idRole = User.roleUser ");
                    Console.WriteLine("Логин\t\tРоль");
                    col = 2;
                }
                else if (table == "Task")
                {
                    list = SQLModule.SelectAllFrom("Task", join: " INNER join User on idUser = userTask INNER join StateTask on stateTask = idState ", attributes: " idTask, nameTask, nameState, loginUser, descriptionTask ", where: where);
                    Console.WriteLine("#\t\tНазвание\tСтатус\t\tПользователь\t\tОписание");
                    col = 5;
                }
                int i = 0;
                int count = 1;
                foreach (string l in list)
                {
                    if (i < col)
                    {
                        i++;
                    }
                    else
                    {
                        i = 1;
                        count++;
                        Console.WriteLine();
                    }

                    Console.Write(l + "\t\t");
                }
                if (list.Count() == 0)
                {
                    count = 0;
                }
                return count;
            }
            catch (Exception ex) {

                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true)) {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };
                return -1;
            }

        }
     

        public static bool delu(string[] keys, string table)
        {
            try
            {
                bool check = true;
                bool flag = true;
                string id = "";

                if (keys.Count() == 1)
                {
                    Console.WriteLine("Проверьте наличие ключей");
                    check = false;
                }
                foreach (string key in keys)
                {
                    List<String> kAtr = key.Split(' ').ToList();
                    kAtr.Remove("");
                    if (flag)
                    {
                        flag = false;

                    }
                    else if (kAtr.Count() == 2)
                    {
                        switch (kAtr[0])
                        {
                            case "id":
                                if (id == "")
                                {
                                    id = kAtr[1];
                                }
                                else
                                {
                                    Console.WriteLine("Повторяется ключ -id");
                                    check = false;
                                }
                                break;
                            default:
                                Console.WriteLine("Неизвестный ключ -" + kAtr[0]);
                                check = false;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Проверьте  корректность -ключ атрибут");
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    if (table == "User")
                    {
                        if (id == UserInfo[1])
                        {
                            Console.WriteLine("Нельзя удалить собственную роль");
                        }
                        else if (SQLModule.SelectAllFrom("User", " where loginUser=" + "\"" + id + "\"", " loginUser ").Count == 0){ 
                            Console.WriteLine("Указанного пользователя не существует");

                        }else if (SQLModule.Delete("User", " where loginUser = \"" + id + "\""))
                        {

                            return true;

                        }
                        else
                        {
                            Console.WriteLine("Не удалось удалить запись, возможно неверно указан id");

                        }
                    }
                    else if (table == "Task")
                    {
                        if (SQLModule.SelectAllFrom("Task", " where idTask=" + "\"" + id + "\"", " idTask ").Count == 0)
                        {
                            Console.WriteLine("Указанной задачи не существует");

                        }else if (SQLModule.Delete("Task", " where idTask = \"" + id + "\""))
                        {
                            return true;

                        }
                        else
                        { 
                            Console.WriteLine("Не удалось удалить запись, возможно неверно указан id");

                        }

                    }
                }

                return false;
            }
            catch (Exception ex) {

                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };
                return false;
            }
        }
        public static bool chu(string[] keys, bool newU)
        {
            try
            {
                bool flag = true;
                bool check = true;
                string lo = "", ln = "", p = "", r = "";
                string attributes = "";

                if (keys.Count() == 1)
                {
                    Console.WriteLine("Проверьте наличие ключей");
                    check = false;
                }
                foreach (string key in keys)
                {
                    List<String> kAtr = key.Split(' ').ToList();
                    kAtr.Remove("");
                    if (flag)
                    {
                        flag = false;

                    }
                    else if (kAtr.Count() == 2)
                    {
                        switch (kAtr[0])
                        {
                            //case "chu":
                            //    break;
                            case "lo":
                                if (lo == "")
                                {
                                    lo = kAtr[1];
                                }
                                else
                                {
                                    Console.WriteLine("Повторяется ключ -lo");
                                    check = false;
                                }

                                break;
                            case "ln":
                                if (ln == "")
                                {
                                    ln = kAtr[1];
                                    attributes += attributes + " loginUser=" + "\"" + ln + "\"" + ",";
                                }
                                else
                                {
                                    Console.WriteLine("Повторяется ключ -ln");
                                    check = false;
                                }
                                break;
                            case "p":
                                if (p == "")
                                {
                                    p = kAtr[1];
                                    attributes = attributes + " passwordUser=" + "\"" + AuthorizationView.Hashing(p) + "\"" + ",";
                                }
                                else
                                {
                                    Console.WriteLine("Повторяется ключ -p");
                                    check = false;
                                }
                                break;
                            case "r":
                                if (r == "")
                                {
                                    r = kAtr[1];
                                    if (r == "a")
                                    {
                                        r = "1";
                                    }
                                    else if (r == "u")
                                    {
                                        r = "2";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Некорректное заполение ключа -r");
                                        check = false;
                                    }
                                    attributes = attributes + " roleUser=" + "\"" + r + "\"" + ",";
                                }
                                else
                                {
                                    Console.WriteLine("Повторяется ключ -r");
                                    check = false;
                                }
                                break;
                            default:
                                Console.WriteLine("Неизвестный ключ -" + kAtr[0]);
                                check = false;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Проверьте  корректность -ключ атрибут");
                        check = false;
                        break;
                    }

                }
                if (check && !newU)
                {
                    if (lo == "" || lo == "   " || lo == " ")
                    {
                        Console.WriteLine("Ключ -lo обязателен к заполению");
                    }
                    else if ((ln == "" || ln == "   " || ln == " ")
                            && (r == "" || r == "   " || r == " ")
                            && (p == "" || p == "   " || p == " "))
                    {
                        Console.WriteLine("Не выбран ни один ключ для изменения");
                    }

                    else if (lo == UserInfo[1] && r == "2")
                    {
                        Console.WriteLine("Нельзя изменить собственную роль");
                    }
                    else if (SQLModule.SelectAllFrom("User", " where loginUser=" + "\"" + lo + "\"", " loginUser ").Count != 0)
                    {

                        return SQLModule.Update("User", attributes.Trim(','), " where loginUser=" + "\"" + lo + "\"");

                    }
                    else
                    {
                        Console.WriteLine("Отсутсвует пользователь с указанным логином");
                    }
                }
                else if (check && newU)
                {
                    if (lo != "")
                    {
                        Console.WriteLine("Неизвестный ключ -lo");
                    }
                    else if (ln == "" || ln == "   " || ln == " ")
                    {
                        Console.WriteLine("Необходимо заполнить ключ ln");
                    }
                    else if (r == "" || r == "   " || r == " ")
                    {
                        Console.WriteLine("Необходимо заполнить ключ r");
                    }
                    else if (p == "" || p == "   " || p == " ")
                    {
                        Console.WriteLine("Необходимо заполнить ключ p");
                    }
                    else if (SQLModule.SelectAllFrom("User", " where loginUser=" + "\"" + ln + "\"", " loginUser ").Count == 0)
                    {
                        attributes = "\"" + ln + "\", \"" + AuthorizationView.Hashing(p) + "\", \"" + r + "\", \"MySalt\"";
                        return SQLModule.InsertInto("User (loginUser, passwordUser, roleUser, saltUser)", attributes.Trim(','));

                    }
                    else
                    {
                        Console.WriteLine("Пользователь с указанным логином уже существует");
                    }
                }
                return false;
            }
            catch (Exception ex) {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                }
                return false;
            }
        }

        public static bool cht(string[] keys, bool newT, bool state)
        {
            try
            {
                bool flag = true;
                bool check = true;
                string id = "", n = "", u = "", d = "", s = "";
                string attributes = "";

                if (keys.Count() == 1)
                {
                    Console.WriteLine("Проверьте наличие ключей");
                    check = false;
                }
                foreach (string key in keys)
                {
                    List<String> kAtr = key.Split(' ').ToList();
                    kAtr.Remove("");
                    if (flag)
                    {
                        flag = false;

                    }
                    else if (kAtr.Count() == 2)
                    {
                        if (state)
                        {
                            switch (kAtr[0])
                            {
                                case "id":
                                    if (id == "")
                                    {
                                        id = kAtr[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -id");
                                        check = false;
                                    }

                                    break;
                                case "s":
                                    if (s == "")
                                    {
                                        s = kAtr[1].Trim();
                                        if (s == "1" || s == "2" || s == "3")
                                        {

                                        }
                                        else
                                        {
                                            Console.WriteLine("Некорректное заполение ключа -s");
                                            check = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -s");
                                        check = false;
                                    }

                                    break;
                                default:
                                    Console.WriteLine("Неизвестный ключ -" + kAtr[0]);
                                    check = false;
                                    break;
                            }
                        }
                        else
                        {
                            switch (kAtr[0])
                            {
                                case "id":
                                    if (id == "")
                                    {
                                        id = kAtr[1];
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -id");
                                        check = false;
                                    }

                                    break;
                                case "n":
                                    if (n == "")
                                    {
                                        n = kAtr[1];
                                        attributes += attributes + " nameTask=" + "\"" + n + "\"" + ",";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -n");
                                        check = false;
                                    }
                                    break;
                                case "u":
                                    if (u == "")
                                    {
                                        u = kAtr[1];
                                        List<string> ulist = SQLModule.SelectAllFrom("User", where: " where loginUser=\"" + u + "\"", attributes: "idUser");
                                        if (ulist.Count != 0)
                                        {
                                            u = ulist[0];
                                            attributes = attributes + " userTask=" + "\"" + ulist[0] + "\"" + ",";

                                        }
                                        else
                                        {
                                            Console.WriteLine("Не существует пользователя " + u);
                                            check = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -u");
                                        check = false;
                                    }
                                    break;
                                case "d":
                                    if (d == "")
                                    {
                                        d = kAtr[1].Trim();
                                        if (d == "1" || d == "0")
                                        {

                                        }
                                        else
                                        {
                                            Console.WriteLine("Некорректное заполение ключа -d");
                                            check = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Повторяется ключ -d");
                                        check = false;
                                    }
                                    break;
                                case "s":
                                    if (state)
                                    {
                                        if (s == "")
                                        {
                                            s = kAtr[1].Trim();
                                            if (s == "1" || s == "2" || s == "3")
                                            {

                                            }
                                            else
                                            {
                                                Console.WriteLine("Некорректное заполение ключа -s");
                                                check = false;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Повторяется ключ -s");
                                            check = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Неизвестный ключ -" + kAtr[0]);
                                        check = false;
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Неизвестный ключ -" + kAtr[0]);
                                    check = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Проверьте  корректность -ключ атрибут");
                        check = false;
                        break;
                    }

                }
                if (check && !newT)
                {
                    if (id == "" || id == "   " || id == " ")
                    {
                        Console.WriteLine("Ключ -id обязателен к заполению");
                    }
                    else if ((s == "" || s == "   " || s == " ") && state)
                    {
                        Console.WriteLine("Ключ -s обязателен к заполению");

                    }
                    else if ((n == "" || n == "   " || n == " ")
                            && (d == "" || d == "   " || d == " ")
                            && (u == "" || u == "   " || u == " ") && !state)
                    {
                        Console.WriteLine("Не выбран ни один ключ для изменения");
                    }

                    else if (d == "1")
                    {
                        Console.WriteLine("Введите описание задачи:");
                        d = Console.ReadLine();
                        attributes += attributes + "descriptionTask=" + "\"" + d + "\"" + ",";
                    }
                    else if (SQLModule.SelectAllFrom("Task", " where idTask=" + "\"" + id + "\"", " idTask ").Count != 0)
                    {
                        if (state && SQLModule.SelectAllFrom("Task", " where idTask=" + "\"" + id + "\" and userTask=" + "\"" + BusinessModule.UserInfo[0] + "\"", " idTask ").Count != 0)
                        {
                            return SQLModule.Update("Task", attributes.Trim(',') + "stateTask = " + s, " where idTask=" + "\"" + id + "\"");

                        }
                        else if (state)
                        {
                            Console.WriteLine("Можно изменить статус только своей задачи");
                        }
                        else
                        {
                            return SQLModule.Update("Task", attributes.Trim(',') + "stateTask = 1", " where idTask=" + "\"" + id + "\"");

                        }

                    }
                    else
                    {
                        Console.WriteLine("Отсутсвует задача с указанным id");
                    }
                }
                else if (check && newT)
                {
                    if (id != "")
                    {
                        Console.WriteLine("Неизвестный ключ -id");
                    }
                    else if (n == "" || n == "   " || n == " ")
                    {
                        Console.WriteLine("Необходимо заполнить ключ n");
                    }
                    else if (u == "" || u == "   " || u == " ")
                    {
                        Console.WriteLine("Необходимо заполнить ключ u");
                    }
                    else if (d.Trim() == "1")
                    {
                        Console.WriteLine("Введите описание задачи:");
                        d = Console.ReadLine();
                    }
                    else

                    {
                        attributes = "\"" + n + "\", \"" + u + "\", \"" + d + "\", 1";
                        return SQLModule.InsertInto("Task (nameTask, userTask, descriptionTask, stateTask)", attributes.Trim(','));

                    }
                }
                return false;
            }
            catch (Exception ex) {

                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                }
                return false;
            }
        }
        


    }
}
