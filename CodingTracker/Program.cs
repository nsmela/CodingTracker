using System;
using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker {

    class Program {

        static void Main(string[] args) {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateTable(CommandBuilder.TABLE_CODING_NAME);

            GetUserInput getUserInput = new();
            getUserInput.MainMenu(databaseManager);
        }

    }
}