using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvFilter;

internal static class CsvService
{
    public static void ReadCsv()
    {
        List<GuestParsed> guestsParsed = new();
        string ouiResterInforme = "Oui";
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/toProcess/
        string folderPath = Path.Combine(projectPath, "files", "toProcess");
        string csvPath = Path.Combine(folderPath, "Guest list 7-minutes-comite-dusine-2025-03-21-20-15 2025-04-11.csv");

        using var reader = new StreamReader(csvPath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Guest>();
        HashSet<string> emails = new();

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
        Console.WriteLine(guestsParsed.Count);
    }
}