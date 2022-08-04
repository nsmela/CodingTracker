using ConsoleTableExt;

namespace CodingTracker {
    internal class TableVisualizer {
        public static void WriteTable<T>(List<T> tableData) where T : class {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle(CommandBuilder.TABLE_CODING_NAME)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }

    }
}