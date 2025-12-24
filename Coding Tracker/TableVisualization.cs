using ConsoleTableExt;
internal class TableVisualization
{
    //internal static void ShowTable(List<Coding> tableData)
    internal static void ShowTable<T>(List<T> tableData) where T : class
    {
        ConsoleTableBuilder
            .From(tableData)
            .WithTitle("Coding")
            .ExportAndWriteLine();
    }
}