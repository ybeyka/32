using System;
using System.Data.SQLite;

class Program
{
    static string connectionString = "Data Source=mydatabase.db;";
    static SQLiteConnection connection = new SQLiteConnection(connectionString);

    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("1. Створити нову базу даних");
            Console.WriteLine("2. Створити таблицю");
            Console.WriteLine("3. Вставити дані");
            Console.WriteLine("4. Оновити дані");
            Console.WriteLine("5. Видалити дані");
            Console.WriteLine("6. Вибірка даних за умовою");
            Console.WriteLine("7. Показати всі дані");
            Console.WriteLine("8. Вийти");
            Console.WriteLine("Введіть вибір:");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    CreateDatabase();
                    break;
                case 2:
                    CreateTable();
                    break;
                case 3:
                    InsertData();
                    break;
                case 4:
                    UpdateData();
                    break;
                case 5:
                    DeleteData();
                    break;
                case 6:
                    SelectData();
                    break;
                case 7:
                    ShowAllData();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Невідомий вибір");
                    break;
            }
        }
    }

    static void CreateDatabase()
    {
        connection.Open();
        connection.Close();
        Console.WriteLine("База даних створена.");
    }

    static void CreateTable()
    {
        Console.WriteLine("Введіть назву таблиці:");
        string tableName = Console.ReadLine();

        Console.WriteLine("Введіть стовпці та типи даних (наприклад, 'ID INTEGER, Name TEXT'):");
        string columns = Console.ReadLine();

        connection.Open();
        string sql = $"CREATE TABLE {tableName} ({columns})";
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
        }
        connection.Close();
        Console.WriteLine($"Таблиця {tableName} створена.");
    }

    static void InsertData()
    {
        Console.WriteLine("Введіть назву таблиці:");
        string tableName = Console.ReadLine();

        Console.WriteLine("Введіть стовпці для вставки (наприклад, 'ID, Name'):");
        string columns = Console.ReadLine();

        Console.WriteLine("Тепер введіть значення для вставки (наприклад, '1, \"John\"'):");
        string values = Console.ReadLine();

        connection.Open();
        string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
        }
        connection.Close();
        Console.WriteLine("Дані були вставлені.");
    }

    static void UpdateData()
    {
        Console.WriteLine("Введіть назву таблиці:");
        string tableName = Console.ReadLine();

        Console.WriteLine("Введіть стовпці та значення для оновлення (наприклад, 'Name = \"John\"'):");
        string setData = Console.ReadLine();

        Console.WriteLine("Введіть умову для оновлення (наприклад, 'ID = 1'):");
        string whereCondition = Console.ReadLine();

        connection.Open();
        string sql = $"UPDATE {tableName} SET {setData} WHERE {whereCondition}";
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
        }
        connection.Close();
        Console.WriteLine("Дані були оновлені.");
    }

    static void DeleteData()
    {
        Console.WriteLine("Введіть назву таблиці:");
        string tableName = Console.ReadLine();

        Console.WriteLine("Введіть умову для видалення (наприклад, 'ID = 1'):");
        string whereCondition = Console.ReadLine();

        connection.Open();
        string sql = $"DELETE FROM {tableName} WHERE {whereCondition}";
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
        }
        connection.Close();
        Console.WriteLine("Дані були видалені.");
    }

    static void SelectData()
    {
        Console.WriteLine("Введіть назву таблиці:");
        string tableName = Console.ReadLine();

        Console.WriteLine("Введіть умову для вибірки (наприклад, 'ID = 1') або залиште поле порожнім для виведення всіх записів:");
        string whereCondition = Console.ReadLine();

        string sql = string.IsNullOrEmpty(whereCondition)
                     ? $"SELECT * FROM {tableName}"
                     : $"SELECT * FROM {tableName} WHERE {whereCondition}";

        connection.Open();
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                    }
                    Console.WriteLine();
                }
            }
        }
        connection.Close();
    }

    static void ShowAllData()
    {
        Console.WriteLine("Введіть назву таблиці для виведення всіх її даних:");
        string tableName = Console.ReadLine();

        string sql = $"SELECT * FROM {tableName}";

        connection.Open();
        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                    }
                    Console.WriteLine();
                }
            }
        }
        connection.Close();
    }
}
