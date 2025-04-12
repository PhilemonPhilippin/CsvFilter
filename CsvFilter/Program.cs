using CsvFilter;

try
{
    CsvService.FilterCsvFiles();
    Console.WriteLine("Parsing successful.");
    Console.WriteLine("Press Enter to exit.");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong.");
    Console.WriteLine("Error : ", ex.Message);
    Console.WriteLine("Press Enter to exit.");
    Console.ReadLine();
}