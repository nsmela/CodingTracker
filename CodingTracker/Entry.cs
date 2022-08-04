namespace CodingTracker {
    internal class Entry {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }

        public string CommandEntryInsert(string tableName) {
            return $"INSERT INTO {tableName} (Date, Duration) VALUES ('{Date.ToString("dd-mm-yy")}' , '{Duration.ToString("h\\:mm")}')";
        }

        public override string ToString() {
            return $"| {Id} | {Date.ToString("dd-mm-yy")} | {Duration.ToString("h\\:mm")} |";
        }
    }
}