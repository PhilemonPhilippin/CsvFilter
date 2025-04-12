using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvFilter;

internal static class CsvService
{

    public static void FilterCsvFiles()
    {
        var guestsParsed = ParseGuestList(ReadCsv());
        Console.WriteLine(guestsParsed.Count);
        WriteCsv(guestsParsed);
    }
    private static IEnumerable<Guest> ReadCsv()
    {
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/toProcess/*myFile*.csv
        string folderPath = Path.Combine(projectPath, "files", "toProcess");
        string csvPath = Path.Combine(folderPath, "Guest list 7-minutes-comite-dusine-2025-03-21-20-15 2025-04-11.csv");

        using var reader = new StreamReader(csvPath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<Guest>().ToList();
    }

    private static List<GuestParsed> ParseGuestList(IEnumerable<Guest> records)
    {
        List<GuestParsed> guestsParsed = new();
        string ouiResterInforme = "Oui";

        HashSet<string> emails = [];

        foreach (var record in records)
        {
            if (emails.Contains(record.Email) == false && record.ResterInforme == ouiResterInforme)
            {
                emails.Add(record.Email);
                guestsParsed.Add(new GuestParsed
                {
                    GuestFirstName = record.GuestFirstName,
                    GuestLastName = record.GuestLastName,
                    Email = record.Email,
                    TelephoneMobile = record.TelephoneMobile,
                    ResterInforme = record.ResterInforme
                });
            }
        }
        return guestsParsed;
    }

    private static void WriteCsv(List<GuestParsed> guestParseds)
    {
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/processed
        string folderPath = Path.Combine(projectPath, "files", "processed");
        string csvPath = Path.Combine(folderPath, "1.csv");

        using var writer = new StreamWriter(csvPath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };

        using var csv = new CsvWriter(writer, config);
        csv.WriteRecords(guestParseds);
    }
}