namespace CodingTracker {
    internal class Entry {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }

        public override string ToString() {
            return $"| {Id} | {Date.ToString(CommandBuilder.DATE_FORMAT)} | {Duration.ToString(CommandBuilder.TIME_FORMAT)} |";
        }
    }
}