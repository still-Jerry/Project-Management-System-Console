using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Project_Management_System_Console.Modules
{
    internal class SQLModule
    {
        public static SQLiteConnection ConnectDB()
        {
            try
            {
                string connectionStr = @"Data Source=ManagementDB.db;";
                SQLiteConnection con = new SQLiteConnection(connectionStr);


                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };

                return null;
            }

        }
        public static List<String> SelectAllFrom(String tables, String where = "", String attributes = "*", String order = "", String join = "")
        {
            try
            {
                List<String> list = new List<String>();

                String cmd = "SELECT " + attributes + " FROM " + tables + join + where + order + ";";
                 SQLiteCommand Command = new  SQLiteCommand(cmd, ConnectDB());
                 SQLiteDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; reader.FieldCount > i; i++)
                    {
                        list.Add(reader[i].ToString());
                    }
                }

                Command.Connection.Close();
                return list;
            }
            catch (Exception ex)
            {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };

                return null;
            }
        }
        public static Boolean Update(String tables, String attributes, String where)
        {
            try
            {
                String cmd = "UPDATE " + tables + " SET " + attributes + where + ";";
                SQLiteCommand Command = new SQLiteCommand(cmd, ConnectDB());
                Command.ExecuteNonQuery();
                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };

                return false;
            }
        }
        public static Boolean InsertInto(String tables, String attributes)
        {
            try
            {
                String cmd = "INSERT INTO  " + tables + " VALUES (" + attributes + ");";
                SQLiteCommand Command = new SQLiteCommand(cmd, ConnectDB());
                Command.ExecuteNonQuery();
                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };
                return false;
            }
        }
        public static Boolean Delete(String tables, String where)
        {
            try
            {
                String cmd = "Delete FROM " + tables + where + ";";
                SQLiteCommand Command = new SQLiteCommand(cmd, ConnectDB());
                Command.ExecuteNonQuery();
                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                ColorModule.ChangeColor(ex.Message, true);

                if (!LogModule.WriteLog(ex.Message, true))
                {
                    ColorModule.ChangeColor("Ошибка записи в файл errors.log", true);

                };

                return false;
            }
        }
    }
}
