using Microsoft.Data.Sqlite;

namespace CodingTracker {
    internal class DatabaseManager {
        private string _connectionString;
        private string _tableName;

        public DatabaseManager(string connectionString) {
            _connectionString = connectionString;
        }

        internal void CreateTable(string tableName) {
            _tableName = tableName;

            using (var connection = new SqliteConnection(_connectionString)) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText =
                        $"CREATE TABLE IF NOT EXISTS {tableName} (" +
                        $"Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        $"Date TEXT," +
                        $"Duration TEXT)";

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        internal void AddEntry(Entry entry) {
            SendCommand(entry.CommandEntryInsert(_tableName));
        }

        internal List<Entry> GetEntries() {
            List<Entry> entries = new();

            using (var connection = new SqliteConnection(_connectionString)) {
                connection.Open();
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $"SELECT * FROM {_tableName}";

                    using (var reader = command.ExecuteReader()) {


                        while (reader.Read()) {
                            Entry entry = new Entry { 
                                Id = reader.GetInt32(0),
                                Date = DateTime.Parse(reader.GetString(1)),
                                Duration = TimeSpan.Parse(reader.GetString(2))};

                            entries.Add(entry);
                        }
                    }
                }
                connection.Close();
            }

            return entries;
        }

        internal Entry GetEntry(int id, SqliteConnection connection) {
            return null;
        }

        void SendCommand(string commandString) {
            using (var connection = new SqliteConnection(_connectionString)) {
                connection.Open();

                using (var command = connection.CreateCommand()) {
                    command.CommandText = commandString;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}