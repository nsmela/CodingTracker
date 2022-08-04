using Microsoft.Data.Sqlite;


namespace CodingTracker {
    internal class DatabaseManager {
        private string _connectionString;
        private string _tableName;

        public DatabaseManager() {
            _connectionString = CommandBuilder.CONNECTION_STRING;
        }

        internal void CreateTable(string tableName) {
            _tableName = tableName;

            string command = CommandBuilder.CreateCodingTable(_tableName);
            SendCommand(command);
        }

        internal void AddEntry(Entry entry) {
            string command = CommandBuilder.InsertCommand(_tableName, entry);
            SendCommand(command);
        }

        internal void UpdateEntry(Entry entry) {
            string command = CommandBuilder.UpdateCommand(_tableName, entry);
            SendCommand(command);
        }

        internal void DeleteEntry(Entry entry) {
            string command = CommandBuilder.DeleteCommand(_tableName, entry);
            SendCommand(command);
        }

        internal List<Entry> GetEntries() {
            List<Entry> entries = new();

            using (var connection = new SqliteConnection(_connectionString)) {
                connection.Open();
                using (var command = connection.CreateCommand()) {
                    command.CommandText = CommandBuilder.SelectAllCommand(_tableName);

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Entry entry = new Entry {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), CommandBuilder.DATE_FORMAT, CommandBuilder.CULTURE_PROVIDER),
                                Duration = TimeSpan.Parse(reader.GetString(2))};

                            entries.Add(entry);
                        }
                    }
                }
                connection.Close();
            }

            return entries;
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