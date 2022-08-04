using System.Configuration;
using System.Globalization;

namespace CodingTracker {
    internal class CommandBuilder {
        public const string DATE_FORMAT = "dd-MM-yy";
        public const string TIME_FORMAT = "h\\:mm";
        public static CultureInfo CULTURE_PROVIDER => CultureInfo.InvariantCulture;

        public static string TABLE_CODING_NAME => ConfigurationManager.AppSettings.Get("TableName");
        public static string CONNECTION_STRING = ConfigurationManager.AppSettings.Get("ConnectionString");

        public static string CreateCodingTable(string tableName) {
            return $"CREATE TABLE IF NOT EXISTS {tableName} (" +
                $"Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                $"Date TEXT," +
                $"Duration TEXT)";
        }

        public static string InsertCommand(string tableName, Entry entry) {
            return $"INSERT INTO {tableName} (Date, Duration) VALUES ('{entry.Date.ToString(DATE_FORMAT)}' , '{entry.Duration.ToString(TIME_FORMAT)}')";
        }

        public static string UpdateCommand(string tableName, Entry entry) {
            return $"UPDATE {tableName} SET " +
                $@"Date = '{entry.Date.ToString(DATE_FORMAT)}', " +
                $@"Duration = '{entry.Duration.ToString(TIME_FORMAT)}' " +
                $@"WHERE ID = {entry.Id}";
        }

        public static string DeleteCommand(string tableName, Entry entry) {
            return $"DELETE from {tableName} WHERE Id = {entry.Id}";
        }

        public static string SelectAllCommand(string tableName) {
            return $"SELECT * from {tableName}";
        }


    }
}