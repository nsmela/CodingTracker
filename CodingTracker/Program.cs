using System;
using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker {

    class Program {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static string tableName = ConfigurationManager.AppSettings.Get("TableName");

        static void Main(string[] args) {
            DatabaseManager databaseManager = new DatabaseManager(connectionString);
            databaseManager.CreateTable(tableName);

            GetUserInput getUserInput = new();
            getUserInput.MainMenu(databaseManager);
        }

    }
}