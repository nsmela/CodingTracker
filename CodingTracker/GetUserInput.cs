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
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case '2':
                        InsertEntry();
                        break;
                    case '3':
                        UpdateEntry();
                        break;
                    case '4':
                        DeleteEntry();
                        break;
                    default:
                        Console.WriteLine("\r\nInvalid entry! Please try again...");
                        Console.ReadKey();
                        break;
                }
            }

            void GetEntries() {
                Console.WriteLine("\r\nTable of Entries: ");
                var entries = _databaseManager.GetEntries();
                foreach (var entry in entries) {
                    Console.WriteLine(entry.ToString());
                }
            }

            void InsertEntry(){
                var date = GetDateInput();
                var duration = GetDurationInput();

                Entry entry = new Entry{ Date = date, Duration = duration };

                _databaseManager.AddEntry(entry);
            }

            void UpdateEntry() {
                GetEntries();
                int id = GetIdInput();
                var date = GetDateInput();
                var duration = GetDurationInput();

                var entry = new Entry{Id = id, Date = date, Duration = duration };
                _databaseManager.UpdateEntry(entry);
                GetEntries();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            void DeleteEntry() {
                GetEntries();
                int id = GetIdInput();

                var entry = new Entry { Id = id };
                _databaseManager.DeleteEntry(entry);
                GetEntries();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            
            int GetIdInput() {
                string result;
                int id;

                while (true) {
                    Console.WriteLine("Enter the ID of the entry to update:");
                    result = Console.ReadLine();

                    if (Int32.TryParse(result, out id)) break;

                    Console.WriteLine("Invalid entry. Please try again.");
                }

                return id;
            }

            DateTime GetDateInput() {
                string result = "";
                DateTime date;

                while (true) {
                    Console.WriteLine("\r\nEnter a date (format: dd-mm-yy):");
                    result = Console.ReadLine();

                    if (DateTime.TryParseExact(result, CommandBuilder.DATE_FORMAT, new CultureInfo("en-US"), DateTimeStyles.None, out date)) break;

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

                    if (TimeSpan.TryParseExact(result, CommandBuilder.TIME_FORMAT, CultureInfo.InvariantCulture, out duration)) break;

                    Console.WriteLine("\r\nInvalid entry!");
                }

                return duration;
            }
        }


    }
}
