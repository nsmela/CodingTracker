using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker {
    internal class GetUserInput {
        private DatabaseManager _databaseManager;
        internal void MainMenu(DatabaseManager databaseManager) {
            _databaseManager = databaseManager;

            string mainMenuString =
                "Welcome to Coding Habits Database" +
                "\n\nPlease Choose an option:" +
                "\r\n0 or Q: Quit" +
                "\r\n1: View Entries" +
                "\r\n2: Add Entry" +
                "\r\n3: Change an Entry" +
                "\r\n4: Delete an Entry";

            while (true) {
                Console.Clear();
                Console.WriteLine(mainMenuString);
                var userChoice = Console.ReadKey().KeyChar;

                switch (userChoice) {
                    case '0':
                    case 'Q':
                    case 'q':
                        Console.WriteLine("\r\nGoodbye!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        return;
                    case '1':
                        GetEntries();
                        Console.ReadKey();
                        break;
                    case '2':
                        AddEntry();
                        break;
                    default:
                        Console.WriteLine("\r\nInvalid entry! Please try again...");
                        Console.ReadKey();
                        break;
                }
            }

            void AddEntry(){
                var date = GetDateInput();
                var duration = GetDurationInput();

                Entry entry = new Entry{ Date = date, Duration = duration };

                _databaseManager.AddEntry(entry);
            }

            void GetEntries() {
                Console.WriteLine("\r\nTable of Entries: ");
                var entries = _databaseManager.GetEntries();
                foreach (var entry in entries) {
                    Console.WriteLine(entry.ToString());
                }
            }

            DateTime GetDateInput() {
                string result = "";
                DateTime date;

                while (true) {
                    Console.WriteLine("\r\nEnter a date (format: dd-mm-yy):");
                    result = Console.ReadLine();

                    if (DateTime.TryParseExact(result, "dd-mm-yy", new CultureInfo("en-US"), DateTimeStyles.None, out date)) break;

                    Console.WriteLine("\r\nInvalid entry!");
                }

                return date;
            }

            TimeSpan GetDurationInput() {
                string result = "";
                TimeSpan duration;

                while (true) {
                    Console.WriteLine("\r\nEnter the duration (format h:mm)");
                    result = Console.ReadLine();

                    if (TimeSpan.TryParseExact(result, "h\\:mm", CultureInfo.InvariantCulture, out duration)) break;

                    Console.WriteLine("\r\nInvalid entry!");
                }

                return duration;
            }
        }
    }
}
